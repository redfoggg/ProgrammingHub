import config from "../config";
import sgMail from "@sendgrid/mail";


sgMail.setApiKey(process.env.SENDGRID_API_KEY);

export default async function sendEmail(type: string = "confirmation", params: any){
    return new Promise((resolve, reject) => {
        const options = {
            from: "no-reply@example.com",
            fromname: "Company Name",
            to: params.email,
            subject: "Subject",
            text: 'Text for email',
            html:'Email'
        }

        switch(type){
            case "confirmation":
                options.subject = "Confirm your Email";
                options.text = `Use the following link to confirm your email \n ${params.link}`
                options.html = `
                    <html>
                        <body>
                            <h3>Company Name</h3>
                            <p>Use the following link to confirm your email <br> ${params.link}</p> 
                        </body>
                    </html>
                `
                break;
            default:
                break;
        }
        
        //To Do add a try catch to verify any errors in the process
        sgMail.send(options);
        
        
    });
}