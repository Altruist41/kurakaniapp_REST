# kurakaniapp -> chat application with REST web services

The server side has been built in PHP with Slim framework, which provides a fast and powerful router that maps route callbacks to specific HTTP request methods and URIs.

The routers used to access the resources via URI are:

get('/totalusers/get/room/:room','getTotalUsersInRoom');
get('/rooms/get/validity/:room','getRoomValidityByDate');
get('/messages/get/room/:room','getMessagesFromRoom');
post('/messages/post', 'postMessage');

The client has been built using C#. 

The MySQL database has been used to store the chat messages.
