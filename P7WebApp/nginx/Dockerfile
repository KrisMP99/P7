
FROM nginx:latest
COPY ./p7webapp.webui/build/ /var/www
COPY ./nginx/nginx.conf /etc/nginx/nginx.conf
ENTRYPOINT ["nginx","-g","daemon off;"]