FROM node:latest

WORKDIR /app

ENV PATH /app/node_modules/.bin:$PATH

# install app dependencies
COPY p7webapp.webui/package.json ./
COPY p7webapp.webui/package-lock.json ./
RUN npm install 
RUN npm install react-scripts -g --silent

COPY p7webapp.webui/ ./



# start app
CMD ["npm", "start"]