function numbersonly( e ) {
	// deal with unicode character sets
	var unicode = e.charCode ? e.charCode : e.keyCode;

	// backspace, tab, or numeric
	if( unicode == 8 || unicode == 9 || ( unicode >= 48 && unicode <= 57 ) ) {
		return true;
	} else {
		return false;
	}
}

function makeAJAXRequest(){			
	var handicap = document.getElementById("txthandicap").value;
	if (!isNaN(handicap)) {
		var args = "handicap="+handicap;
	} else {
		var args = "";
	}
	sendAjaxRequest("GET", "http://localhost/ARQSI/AnalisaMercados/analisar.php", analisarHandler, args);
	sendUserFeedBack('Pedido Enviado', 'P.F. aguarde pela resposta do servidor');
}

function createXmlHttpRequest() {
	var xmlHttp = null;

	if (window.XMLHttpRequest)
	{// code for IE7+, Firefox, Chrome, Opera, Safari
		xmlHttp = new XMLHttpRequest();
	}
	else if(window.ActiveXObject)
	{// code for IE6, IE5
		xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
	}

	return xmlHttp;
}

function sendAjaxRequest(method, url, handler, args) {
	var xmlHttp = null;
	xmlHttp = createXmlHttpRequest();

	if(!args){
		args = null;
	}

	if(xmlHttp != null) {
		if (method.toLowerCase()=="post"){
			xmlHttp.open(method, url, true);
			xmlHttp.onreadystatechange = function() {
				handler(xmlHttp);
			};
			xmlHttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
			xmlHttp.send(args);
		}else{
			xmlHttp.open(method, url+"?"+args, true);
			xmlHttp.onreadystatechange = function() {
				handler(xmlHttp);
			};
			xmlHttp.send(null);
		}
	}
}

function analisarHandler(requestXmlHttpObj) {
	var feedbackTitle = document.getElementById("userfeedbacktitle");
	var feedbackdiv = document.getElementById("userfeedbackmessage");
	if (requestXmlHttpObj.readyState==4 && requestXmlHttpObj.status == 200) {

		var texto = requestXmlHttpObj.responseText;
		if (texto == 'OK') {		
			sendUserFeedBack("Opera&ccedil;&atilde;o concluida", "N&atilde;o esque&ccedil;a que as altera&ccedil;&otilde;es s&oacute; podem ser executadas uma vez por dia.");
		} else {
			sendUserFeedBack("Ocorreu um Erro", texto);
		}
	}
}

function sendUserFeedBack(title, text){			
	var feedbackTitle = document.getElementById("userfeedbacktitle");
	var feedbackdiv = document.getElementById("userfeedbackmessage");
	feedbackTitle.innerHTML = title;
	feedbackdiv.innerHTML = text;	
}
