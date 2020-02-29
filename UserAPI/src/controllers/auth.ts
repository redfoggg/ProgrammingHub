import {Request, Response} from "express";
import {AuthService} from "../services/AuthService";

const authService = new AuthService();

/**
 * @param req
 * @param res
 *  
 */

export async function register(req: Request, res: Response){
    const { email, password, firstName, lastName } = req.body;
    try{
        const result = await authService.register(email, password, firstName, lastName);
        console.log(`User: ${email} has been created`);
        res.json({
            message: "User created",
            data: result
        });
    } catch(err){
        console.log(err);
        if(String(err).includes("Username already exist")){
            return res.status(400).json({
                error: true,
                message: "Email is taken"
            });
        }
    }
    res.status(500).json({
        error: true,
        message: "Internal server error"
    })
 }

export async function confirmEmail(req: Request, res: Response){
    const email = req.query.email;
    const code = req.query.c;

    try{
        const confirmed = await authService.confirmEmail(email, code);
        res.json({
            isUserConfirmed: true,
            message: confirmed
        });
    } catch(err){
        console.log(err);
        if(String(err).includes("Code has expired") || String(err).includes("Code does not match")){
            return res.status(400).json({
                isUserConfirmed: false,
                message: "Invalid token"
            });
        }
        res.status(500).json({
            error: true,
            message: "An internal server error has occurred"
        });
    }
}