# UserAPI

This is a RESTful API that handles the register flow in this web application with email verifification process in between.

This piece of software can be used in any other project separaly.

## Setup

You need to set a ormconfig.json file, and pass the credentials and DB you are using, this API is using Postgresql but you can change this to any DB you want to.

There is a need to use a .env file with your settings paramaters from DB, SendGrid, Redis and what port your node will be using , if you dont want to use .env file, you will need to change some of the call's in this code.

## Dependency's

* Redis

* Postgresql

* SendGrid

* node v12.14.1

## Usage

```Bash 
node run serve
```
For build and run the server or
```Bash
node run start
```
For run the index.ts file directly

## License
[MIT](https://choosealicense.com/licenses/mit/)