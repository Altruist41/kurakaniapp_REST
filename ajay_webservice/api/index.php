<?php
	include 'db.php';
	require 'Slim/Slim.php';
	\Slim\Slim::registerAutoloader();

	$app = new \Slim\Slim();

	$app->get('/totalusers/get/room/:room','getTotalUsersInRoom');
	$app->get('/rooms/get/validity/:room','getRoomValidityByDate');
	$app->get('/messages/get/room/:room','getMessagesFromRoom');
	$app->post('/messages/post', 'postMessage');

	$app->run();

	// GET http://localhost:1234/ajay/api/totalusers/get/room/1
	function getTotalUsersInRoom($room)
	{
		$sql = "SELECT count(DISTINCT user) as count FROM chat WHERE room=".$room.";";
		try
		{
			$db = getDB();
			$stmt = $db->query($sql);
			$users = $stmt->fetchAll(PDO::FETCH_OBJ);
			$db = null;
			echo json_encode($users);
		}
		catch(PDOException $e)
		{
			echo '{"error":{"text":'. $e->getMessage() .'}}'; 
		}
	}

	// GET http://localhost:1234/ajay/api/rooms/get/validity/1
	function getRoomValidityByDate($room)
	{
		$sql = "SELECT max(postdate) as date FROM chat WHERE room=".$room.";";
		try
		{
			$db = getDB();
			$stmt = $db->query($sql);
			$date = $stmt->fetchAll(PDO::FETCH_OBJ);
			$now = time();

			if($date[0]->date!=NULL)
			{
				$datediff = $now - strtotime($date[0]->date);
				$days = floor($datediff / (60 * 60 * 24));
			}
			else
			{
				$days = 0;
			}

			$db = null;
			
			if($days < 8)
			{
				echo '{"validity":"valid"}';
			}
			else
			{
				cleanRoom($room);
				echo '{"validity":"invalid"}';
			}
		}
		catch(PDOException $e)
		{
			echo '{"error1":{"text":'. $e->getMessage() .'}}'; 
		}
	}

	function cleanRoom($room)
	{
		$sql = "DELETE FROM chat WHERE room=:room_no;";
		try
		{
			$db = getDB();
			$stmt = $db->prepare($sql);  
			$stmt->bindParam("room_no", $room);
			$stmt->execute();
			$db = null;
		}
		catch(PDOException $e)
		{
			// nothing
		}
	}

	// GET http://localhost:1234/ajay/api/messages/get/room/1
	function getMessagesFromRoom($room)
	{
		$sql = "SELECT * FROM chat WHERE room=".$room.";";
		try
		{
			$db = getDB();
			$stmt = $db->prepare($sql); 
			$stmt->execute();		
			$updates = $stmt->fetchAll(PDO::FETCH_OBJ);
			$db = null;
			echo json_encode($updates);			
		}
		catch(PDOException $e)
		{
			echo '{"error":{"text":'. $e->getMessage() .'}}'; 
		}
	}

	// POST http://localhost:1234/ajay/api/messages/post
	function postMessage()
	{
		$request = \Slim\Slim::getInstance()->request();
		$data = json_decode($request->getBody());
		$sql = "INSERT INTO chat VALUES ('', :user, :message, :room, CURRENT_TIMESTAMP);";
		
		try {
			$db = getDB();
			$stmt = $db->prepare($sql);  
			$stmt->bindParam("user", $data->user);
			$stmt->bindParam("message", $data->message);
			$stmt->bindParam("room", $data->room);
			$stmt->execute();
			$db = null;
		}
		catch(PDOException $e)
		{
			echo '{"error":{"text":'. $e->getMessage() .'}}'; 
		}
	}
?>