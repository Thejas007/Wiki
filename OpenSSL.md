# Create self signed cert

- openssl req -nodes -x509 -newkey rsa:4096 -keyout key.pem -out cert.pem -days 1

- openssl pkcs12 -export -out dev.pfx -inkey key.pem -in cert.pem
