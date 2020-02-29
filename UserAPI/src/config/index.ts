import "reflect-metadata";
import * as dotenv from "dotenv";
import { createTransport } from "nodemailer";
import redis from "redis";

const redisClient = redis.createClient({
    port: parseInt(process.env.REDIS_PORT)
})

const smtp = {
    HOST: process.env.SMTP_HOST,
    PORT: process.env.SMTP_PORT,
    USERNAME: process.env.SMTP_USERNAME,
    PASSWORD: process.env.SMTP_PASSWORD,
};

const transport = createTransport({
    host: smtp.HOST,
    port: parseInt(smtp.PORT),
    auth:{
        user: smtp.USERNAME,
        pass: smtp.PASSWORD 
    }
});


dotenv.config();

export default{
    NODE_ENV: process.env.NODE_ENV,
    APP_NAME: process.env.APP_NAME,
    PORT: process.env.PORT,
    APP_DOMAIN: process.env.APP_DOMAIN,
    SMTP: {
        options: smtp,
        transporter: transport
    },
    REDIS_CLIENT: redisClient
};

