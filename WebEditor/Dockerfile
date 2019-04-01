FROM node:lts-alpine as build-stage
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

FROM nginx:stable-alpine as production-stage
COPY --from=build-stage /app/dist /usr/share/nginx/html

###
#TODO качать релиз из unity-repo
COPY --from=build-stage /app/src/unity /usr/share/nginx/html/unity
###

COPY --from=build-stage /app/default.conf /etc/nginx/conf.d/default.conf
COPY --from=build-stage /app/nginx/ /etc/nginx/

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]