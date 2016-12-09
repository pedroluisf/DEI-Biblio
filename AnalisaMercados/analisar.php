<?php

	$resultadoWs='';

	try {
	
		$proxy = new SoapClient('http://localhost:2008/WSIDEI.asmx?WSDL');

		$resposta = $proxy->getCatalog();
		$Produtos = $resposta->getCatalogResult->Produto;

		if (isset($_GET['handicap']) && is_numeric($_GET['handicap'])){
			$hc = $_GET['handicap'];
		} else {
			$hc = 4;
		}

		$i=0;
		foreach ($Produtos as $Produto)
		{
			if ($i % $hc == 0){
				$Produto->Preco = round($Produto->Preco * (1 - rand(0, 50) / 100), 2);
			}
			$i++;
		}
		$proxy->setPrices(array('prodList' => $Produtos));		

		$resultadoWs='OK';
		
	} catch (Exception $e) {
	
	  $resultadoWs=$e->getMessage();
	  
	}
	
	echo ($resultadoWs);
		

?>
