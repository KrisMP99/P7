FROM node:latest as base
WORKDIR /app
ENV PATH /app/node_modules/.bin:$PATH
# install app dependencies
COPY p7webapp.webui/package.json ./
COPY p7webapp.webui/package-lock.json ./

FROM base as development
RUN npm install 
RUN npm install react-scripts -g
CMD ["npm", "start"]

FROM base as build
RUN npm ci --silent
RUN npm install react-scripts -g
COPY /p7webapp.webui/ .
RUN npm run build
