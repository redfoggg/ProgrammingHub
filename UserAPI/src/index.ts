import express from "express";
import compression from "compression";
import helmet from "helmet";
import morgan from "morgan";
import { createConnection } from "typeorm";

import config from "./config";

import * as validatorMiddleware from "./middleware/validator";
import * as authController from "./controllers/auth";

//if dont have any @types
var logger = require('debug-logger');

const app = express();

//Add middlewares
app.use(express.json());
app.use(express.urlencoded({extended: false}));
app.use(helmet());
app.use(compression());
app.use(morgan(':response-time ms - :remote-addr - :remote-user [:date[clf]] ":method :url HTTP/:http-version" :status :res[content-length] ":referrer" ":user-agent"'));

const port = config.PORT || 3000;

//anonymous self-invoking async function
(async () => {
    try {
        const connection = await createConnection();
        logger.debug("Connection to Database established");
    } catch(err){
        logger.error("Connection to Database Failed: " + err);
    }

    app.get("/confirmemail", validatorMiddleware.confirmEmail, authController.confirmEmail);

    app.post("/register", validatorMiddleware.register, authController.register);

    app.post("*", (req, res) => {
        res.status(404).json({
            error: true,
            message: "Not Found"
        });
    });

    const port = config.PORT || 3000;
    app.listen(port, () => {
        logger.debug(`Listening on port ${port}...`);
    });
})();


export default app;

