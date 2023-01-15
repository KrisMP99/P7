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

FROM nginx:1.23-alpine as basenginx
FROM basenginx as builder

RUN apk add --no-cache \
  # nginx
  gcc \
  libc-dev \
  make \
  openssl-dev \
  pcre-dev \
  zlib-dev \
  linux-headers \
  curl \
  gnupg \
  libxslt-dev \
  gd-dev \
  # libjwt
  jansson-dev \
  autoconf \
  automake \
  libtool \
  cmake \
  check-dev \
  unzip

ARG JWT_MODULE_PATH=/usr/local/lib/ngx-http-auth-jwt-module
RUN mkdir -p $JWT_MODULE_PATH/src

RUN mkdir libjwt \
  && curl -sL https://github.com/benmcollins/libjwt/archive/v1.13.1.tar.gz \
   | tar -zx -C libjwt/ --strip-components=1 \
  && cd libjwt \
  && autoreconf -i \
  && ./configure \
  && make all \
  && make check \
  && make install

RUN curl -fSL http://nginx.org/download/nginx-1.23.3.tar.gz -o nginx.tar.gz \
  && mkdir -p /usr/src \
  && tar -zxC /usr/src -f nginx.tar.gz \
  && rm nginx.tar.gz

ADD p7webapp.webui/nginx/jwt/config $JWT_MODULE_PATH/config
ADD p7webapp.webui/nginx/jwt/src $JWT_MODULE_PATH/src

RUN mkdir -p /home/ngx-cache && cd /home/ngx-cache && \
    wget https://github.com/nginx-modules/ngx_cache_purge/archive/refs/tags/2.5.2.zip && \
    unzip 2.5.2.zip

RUN cd /usr/src/nginx-1.23.3 \
  && ./configure --with-compat --add-dynamic-module=$JWT_MODULE_PATH --add-dynamic-module=/home/ngx-cache/ngx_cache_purge-2.5.2 \
  && make modules

FROM builder as production
ARG LIBJWT=libjwt.so.1.8.1

COPY --from=builder /usr/src/nginx-1.23.3/objs/ngx_http_auth_jwt_module.so /usr/lib/nginx/modules/ngx_http_auth_jwt_module.so
COPY --from=builder /usr/src/nginx-1.23.3/objs/ngx_http_cache_purge_module.so /usr/lib/nginx/modules/ngx_http_cache_purge_module.so
COPY --from=builder /usr/local/lib/${LIBJWT} /lib

COPY --from=build /app/build/ /var/www
COPY --from=build /app/nginx/nginx.conf /etc/nginx/nginx.conf
ENTRYPOINT ["nginx","-g","daemon off;"]
