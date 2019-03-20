# Sombra

[![Build Status](https://travis-ci.org/markslingerland/Sombra.svg?branch=master)](https://travis-ci.org/markslingerland/Sombra)

Sombra is an open-source crowdfunding platform for charities.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

What things you need to install the software and how to install them

* [Docker](https://www.docker.com/get-docker)

Without Docker:

* [.NET Core 2.1-Preview2](https://www.microsoft.com/net/download/dotnet-core/sdk-2.1.300-preview2)
* [RabbitMQ](https://www.rabbitmq.com/download.html)
* SQLEXPRESS instance running

### Installing

A step by step series of examples that tell you have to get a development env running

Using Docker

```
Open the solution and run the docker-compose up command

Reach the web solution on http://localhost:80
```

Or if you're not using Docker

```
Make sure RabbitMQ is running and a SQLEXPRESS instance

dotnet run for the web project and for every individual service
```

End with an example of getting some data out of the system or using it for a little demo

## Running the tests

All the the unit tests are being run during build time (Docker).
To run the individual test project user the command 
```
dotnet test 
```

## Deployment

With the complete repo on your server or pc you can use this command to start all the services.

```
docker-compose up
```

## Built With

* [EasyNetQ](https://github.com/EasyNetQ/EasyNetQ/wiki/Introduction) - The framewore used to interact with RabbitMQ

## Authors

* **Mark Slingerland** - *Back-end development* - [markslingerland](https://github.com/markslingerland)
* **Jelle Kerkstra** - *Back-end development* - [JelleKerkstra](https://github.com/JelleKerkstra)
* **Ruben Everwijn** - *Back-end/Front-end development* - [rubenev](https://github.com/rubenev)
* **Stef de Kramer** - *Front-end development* - [SteffdeKramer](https://github.com/SteffdeKramer)
* **Daisy Hofstede** - *Digital designer* - [daisyhofstede](https://www.linkedin.com/in/daisyhofstede/)
* **Tessa Hamel** - *Digital designer* - [tessahamel](https://www.linkedin.com/in/tessa-hamel-8931609a/)
* **Emmelie de Jong** - *Project manager / Digital strategist* - [emmeliedejong](https://www.linkedin.com/in/emmelie-de-jong-3b8431135/)

See also the list of [contributors](https://github.com/markslingerland/sombra/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

