<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" type="text/css" href="css/analisa.css" />
<title>Analisa Mercados</title>
<script type="text/javascript" src="js/javascript.js"></script>
</head>

<body>
    <div id="page">
			<br />		  
	</div>
    <div id="mainPicture">
    	<div class="picture">
        	<div id="headerTitle">Analisa Mercados</div>
            <div id="headerSubtext">A sua empresa de verifica&ccedil;&atilde;o de pre&ccedil;os</div>
        </div>
    </div>
    <div class="contentBox">
    <div class="innerBox">
        <div class="contentTitle">Analisar IDEIBiblio</div>
        <div class="contentText">
			<p>A empresa "Analisa Mercados" tem como miss&atilde;o estudar a situa&ccedil;&atilde;o comercial de um cat&atilde;logo de Produtos fornecido pelo cliente.</p> 
			<p>Atrav&eacute;s de uma pesquisa a diversas lojas da concorr&ecirc;ncia o estudo de mercado ir&aacute; propor um novo pre&ccedil;o que fa&ccedil;a uma concorr&ecirc;ncia competitiva com os restantes distribuidores existentes no mercado.</p> 
			<br />		  
			<p>Na pr&aacute;tica o pre&ccedil;o proposto &eacute; sugerido atrav&eacute;s de uma simples varia&ccedil;&atilde;o aleat&oacute;ria do pre&ccedil;o original num intervalo de 0% a 50%.</p> 
			<br />		  
			<p>Click no bot&atilde;o para efectuar uma an&aacute;lise de mercados aos produtos recebidos em cat&aacute;logo pelo cliente</p> 
			<p>"IDEI Biblioteca Online"</p>
			<br />		  
			<form action="index.php" method="POST">
				<input type="hidden" name="analisar" value="1">
				Afectar um produto em cada: <input name="handicap" id="txthandicap" size="3" type="text" value="4" onkeypress="return numbersonly(event);"/>
				<input value="Analisar" type="button" onClick="makeAJAXRequest()" style="background-color:white;"/>
			</form>
        </div>
        <div id="userfeedbacktitle" class="contentTitle"></div>
        <div id="userfeedbackmessage" class="contentText"></div>
    </div>
    <div id="footer">Projecto desenvolvido por Pedro Luis Ferreira (1090702) e Pedro Magueija (1091022) para a disciplina ARQSI do <a href="http://www.dei.isep.ipp.pt">DEI</a> </div>
</body>
</html>
