FROM node:latest AS base
WORKDIR /app
ENV PATH /app/node_modules/.bin:$PATH
COPY p7webapp.webui/package.json ./
COPY p7webapp.webui/package-lock.json ./

FROM base AS development
WORKDIR /app
RUN npm install 
RUN npm install react-scripts -g
CMD ["npm", "start"]

FROM base AS build
RUN npm ci --silent
RUN npm install react-scripts -g
COPY /p7webapp.webui/ .
RUN npm run build

FROM nginx:latest AS production
COPY --from=build /app/build/ /var/www
COPY --from=build /app/nginx/nginx.conf /etc/nginx/nginx.conf
ENTRYPOINT ["nginx","-g","daemon off;"]