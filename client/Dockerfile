FROM node:22.14.0-alpine3.21
WORKDIR /App

COPY . .
RUN npm install
RUN npm run build

# For project purposes, we will use the vite 
# server. For production, we would deploy with an 
CMD ["npm", "run", "preview"]