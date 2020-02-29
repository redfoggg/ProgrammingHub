import * as argon2 from "argon2";
import crypto from "crypto";
import uuid from "uuid";
import config from "../config";
import sendEmail from "../utils/sendEmail";

import { User } from "../entity/User";
import { rejects } from "assert";

export class AuthService {
    constructor(){}

    public async register(email: string, password: string, firstName: string, lastName: string){
        const doesUserExist = await User.findOne({ email: email })

        if(doesUserExist !== undefined)
        {
            if(doesUserExist.email === email)
                throw new Error("Username taken");
        }
        
        const uid = uuid.v1();
        //Fixed 16 caratcthers salt
        const salt = this.generateSalt().substring(0, 15);
        
        try{
            const passwordHashed = await argon2.hash(salt + password);

            const user = new User();
            user.firstName = firstName.trim();
            user.lastName = lastName.trim();
            user.email = email.trim();
            user.password = passwordHashed;
            user.isActive = true;
            user.uid = uid;
            user.salt = salt;
            user.isUserConfirmed = false;
            await user.save();
            this.sendConfirmationEmail(email.trim());
        }
        catch(err){
            throw new Error(err);
        }

        return { email, firstName, lastName, uid};
    }

    generateSalt(){
        return crypto.randomBytes(16).toString("hex");
    }

    async sendConfirmationEmail(email: string){
        const redisClient = config.REDIS_CLIENT;
        const uniqueCode = this.generateSalt();
        //add to rediscliente
        redisClient.setex(`confirmation_${email}`, 60*60*24, uniqueCode);
        console.log(uniqueCode);

        const link = `${config.APP_DOMAIN}/confirmemail?email=${email}&c=${uniqueCode}`;
        try{
            await sendEmail("confirmation", {
                email: email,
                link
            });
            console.log("Email sent");
        } catch(err){
            console.log(`Error sending confirmation email to ${email}: ${err}`);
            throw new Error(`Error sending confirmation email to ${email}: ${err}`);
        }
    }

    confirmEmail(email: string, code: string) {
        return new Promise((resolve, reject) => {
            const redisClient = config.REDIS_CLIENT
            const redisKey = `confirmation_${email}`
            redisClient.get(redisKey, async (err, data) => {
                if(err) {
                    console.log(`Redis error: ${err}`)
                    reject(`Redis error: ${err}`)
                }
  
                if(data) {
                    if(data === code) {
                        const user = await User.findOne({ email: email })
  
                        user.isUserConfirmed = true
                        user.save().then(() => {
                            redisClient.del(redisKey, (err, reply) => {
                                // Do not return an error because this is not a crucial step
                                if(err) console.log("Redis delete error: " + err)
                            })
                            // Delete the token from redis
                            resolve("User confirmed")
                        }).catch((err) => {
                            reject(`Could not save to DB: ${err}`)
                        })
                    } else{
                        reject("Code does not match")
                    }
                } else {
                    reject("Code has expired or does not exist")
                }
            })
        })
    }

}