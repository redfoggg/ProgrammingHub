import { Request, Response, NextFunction } from "express";
import Joi from "joi";

const registerSchema = Joi.object().keys({
    "email": Joi.string().email().required(),
    "password": Joi.string().min(8).max(128).required(),
    "firstName": Joi.string().max(50).required(),
    "lastName": Joi.string().max(50).required()
});


const confirmEmailSchema = Joi.object().keys({
    "email": Joi.string().email().required(),
    "c": Joi.string().min(16).required()
});

export function confirmEmail(req: Request, res: Response, next: NextFunction) {
    const result = Joi.validate(req.query, confirmEmailSchema);

    if(result.error !== null) {
        console.log(result.error);
        return res.status(400).json({
            error: true,
            message: "Invalid input parameters"
        });
    }

    next();
}


export function register(req: Request, res: Response, next: NextFunction){
    const result = Joi.validate(req.body, registerSchema);

    if(result.error !== null){
        console.log(result.error);
        return res.status(400).json({
            error: true,
            message: "Invalid paramaters"
        });
    }
    next();
}