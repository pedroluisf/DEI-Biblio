<?php

	define ('UNIT_PRICE', '2.5');

	function calculateShippingFees($numberBooks) { 

		$params =  get_object_vars($numberBooks);		
		
		if ($params["numberBooks"] <= 0) { return array("calculateShippingFeesResult" => "0.0"); }

		$result = (double) UNIT_PRICE * $params["numberBooks"];
		return array("calculateShippingFeesResult" => $result);

	}

	//$server = new SoapServer(null, array('location' => "http://localhost/ARQSI/ShippingAll/ShippingAll.php",'uri'=>"http://localhost/ARQSI/ShippingAll/ShippingAll.wsdl"));
	$server = new SoapServer("http://localhost/ARQSI/ShippingAll/ShippingAll.wsdl");
	
	$server->addFunction("calculateShippingFees");
	
	$server->handle()
	
?>