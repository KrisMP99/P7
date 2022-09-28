
FROM nginx:latest
COPY ./p7webapp.webui/build/ /var/www
COPY ./nginx/nginx.conf /etc/nginx/nginx.conf
EXPOSE 3000
ENTRYPOINT ["nginx","-g","daemon off;"]