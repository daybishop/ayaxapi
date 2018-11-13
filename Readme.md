git clone https://github.com/daybishop/ayaxapi.git
cd ayaxapi/AyaxApi
sudo docker build -t aspnetapp .
sudo docker run -it --rm -p 5000:80 --name aspnetcore_ayax aspnetapp