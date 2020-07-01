# kurakaniapp -> chat application with REST web services

The server side has been built in PHP with Slim framework, which provides a fast and powerful router that maps route callbacks to specific HTTP request methods and URIs.

The routers used to access the resources via URI are:

get('/totalusers/get/room/:room','getTotalUsersInRoom'); "http://localhost:1234/ajay/api/totalusers/get/room/"; 
get('/rooms/get/validity/:room','getRoomValidityByDate'); "http://localhost:1234/ajay/api/rooms/get/validity/"; 
get('/messages/get/room/:room','getMessagesFromRoom'); "http://localhost:1234/ajay/api/messages/get/room/";
post('/messages/post', 'postMessage'); "http://localhost:1234/ajay/api/messages/post";

The client has been built using C#. 

The MySQL database has been used to store the chat messages.
