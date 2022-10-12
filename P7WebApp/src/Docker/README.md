# Running the Project in Docker
To be able to run the project in Docker, and test it for deployment, you must first [install Docker](https://docs.docker.com/get-docker/).
Afterwards, either open Docker Desktop (Windows / Mac) to start the service, or on Linux run the command `sudo service docker start`.

## Development
When developing, you can freely develop outside of the Docker container. However, if you wish to use Docker for developing, do the following:
1. `cd` into the `p7webapp.webui` folder, and run the command `npm install`
1. Now, `cd` back into the `Docker` folder
1. If you haven't already, create a `.env` file and add `TARGET_USER="username" and TARGET_PASSWORD="password"`, which is credentials used for the PostgreSQL database. Remember to change the username and password.
1. Wait for the images to build and the containers to start
1. You can now access the UI at `localhost:3000` or the API at `localhost:5000`

**NB!**
Hot reload does not currently work in the Docker container. To fix this, open your `package.json` file in the `p7webaap.webui` folder, and find the `"start": "react-scripts start"` and change it to `"start": "WATCHPACK_POLLING=true react-scripts start"` 
This will require you to rebuild the image for the webui project and the container.

## Production
Before you can merge your work into the `dev` brach (and therefore also the `main` branch), you have to test your code in a production container envrionment.
In order to set up Docker for production, do the following:
1. `cd` into the `p7webapp.webui` folder, and run the command `npm install`
1. Now, `cd` back into the `Docker` folder
1. If you haven't already, create a `.env` file and add `TARGET_USER="username" and TARGET_PASSWORD="password"`, which is credentials used for the PostgreSQL database. Remember to change the username and password.
1. Run the command: `docker-compose -f docker-compose.prod.yml up -d` *Note:* On Linux you might have to include `sudo` in front of the command
1. Wait for the images to build and the containers to start
1. You can now access both the UI and the API on `localhost`
   - This is due to NGINX redirecting the requests from `localhost` to either the UI or API automatically.


## Deployment
For deployment we use [Docker Swarm](https://docs.docker.com/engine/swarm/) to scale, manage and load balance the Docker containers across our servers.
You can test the deployment locally on your own machine, but this is not needed, refer to the [production setup](#Deployment).
If you decide to test it locally however, make sure you have enough available RAM, as several containers will be created.
1. `cd` into the `p7webapp.webui` folder, and run the command `npm install`
1. Now, `cd` back into the `Docker` folder
1. If you haven't already, create a `.env` file and add `TARGET_USER="username" and TARGET_PASSWORD="password"`, which is credentials used for the PostgreSQL database. Remember to change the username and password.
1. Run the command `docker swarm init`. This will generate a join-token, which you can copy-paste to any servers you'd like to connect to the swarm. Follow the instructions given in the terminal. *Note:* On Linux, you might have to include `sudo`.
1. *OPTIONAL:* Edit the `docker-compose.prod.yml` and change the `replicas` tag for the services to how many containers you would like to start.
1. Run the command: `docker stack deploy -c docker-compose.prod.yml P7`. You can change `P7` to whatever you like. *Note:* On Linux you might have to include `sudo` in front of the command
1. Wait for the images to build and the containers to start.
1. You can now access both the UI and the API on `localhost` (if running locally), or the IP of the server you have deployed to.


# NGINX
We use [NGINX](https://www.nginx.com/) as an reverse proxy, as [Docker Swarm](https://docs.docker.com/engine/swarm/) handles the load balancing.
To add an endpoint to NGINX, edit the `nginx.conf` file located in `/p7webapp.webui/nginx/nginx.conf/`, and add a new `location` object. For example:<br>
`location /test/ { proxy_pass http://web-api/; }`, and you can now access the backend on `{server-ip}/test/`. *Note*, the `http://web-api/` refers to an `upstream` object, which routes the traffic to the `p7webapp_api` Docker service.
If a new service is created for handling API calls, a new upstream object with the service name must be created.

Current, the endpoint `{server-ip}/api/` refers to the backend service. 