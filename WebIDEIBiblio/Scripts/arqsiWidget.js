var METHOD = "GET";
var REQUEST_COMPLETED = 4;
var HTTP_STATUS_OK = 200;
var HOST = 'http://phpdev.dei.isep.ipp.pt/i091022/';
var currRec = 0;
var recPag = 5;
var servicos = {
  categorias:HOST + 'categorias.php',
  livros:HOST + 'livros.php',
  info:HOST + 'info.php'
};
var bookList = new Array();

/**
 * Funcao chamada no load do documento HTML.
 */
function loadWidget() {
  var div = document.getElementById('arqsiWidget');
  div.innerHTML =''; 
  if (div.tagName == 'DIV') {     
    sendCategoriasRequest();      
    buildMenuStrip();
  }
}

/**
 * Funcao que carrega os elementos para a select box de categorias.
 * Select apenas é construida em caso de sucesso de callback
 */
function sendCategoriasRequest() {
  sendAjaxRequest(METHOD, servicos.categorias, afterGetCategoriesComplete);
}

/**
 * Funcao que constroi a area Menu
 */
function buildMenuStrip(){
  var div = document.getElementById('arqsiWidget');
  var myTableDiv = document.createElement('div');
  myTableDiv.id = "arqsiWidgetMenu_div";
  myTableDiv.className = "menu";
  div.appendChild(myTableDiv);

  var strInnerHTML = "<table id='arqsiWidgetMenu' class='menu'>";
  strInnerHTML += "<thead><tr>";
  strInnerHTML += " <th width='200' ><div id='arqsiWidgetLogo_div'></th>";
  strInnerHTML += " <th width='400' colspan='2'>Efectue a sua pesquisa de Livros</th>";
  strInnerHTML += "</tr></thead>";
  strInnerHTML += "<tbody><tr>";
  strInnerHTML += " <td width='200'><div id='arqsiWidgetBookNews_div'></td>";
  strInnerHTML += " <td width='200'><div id='arqsiWidgetBookLimit_div'></td>";
  strInnerHTML += " <td width='200'><div id='arqsiWidgetCategoriasSelect_div'></td></tr>";
  strInnerHTML += "</tr>";
  strInnerHTML += "<tr>";
  strInnerHTML += " <td width='600' colspan='3'><div id='arqsiWidgetBookSearch_div'></td>";
  strInnerHTML += "</tr>";
  strInnerHTML += "</tbody></table>";
  myTableDiv.innerHTML = strInnerHTML;

  createLogo();
  createBookNewsArea();
  createBookLimitArea();
  createBookSearchArea();
}

/**
 * Funcao que constroi a area do logotipo Imagem.
 */
function createLogo(){
  var myDiv = document.getElementById('arqsiWidgetLogo_div');  
  //myDiv.style.top = "top";
  var a = document.createElement("a");
  var logo = document.createElement("img");
  logo.src  = HOST + 'images/logo_ISEP_2_transparente.png';
  logo.style.height = 50;
  logo.style.border = 0;
  a.href  = 'http://dei.isep.ipp.pt';
  a.appendChild(logo);
  myDiv.appendChild(a);
}

/**
 * Funcao que constroi a area de pesquisa de novidades.
 */
function createBookNewsArea(){
  var myDiv = document.getElementById('arqsiWidgetBookNews_div');  

  var a = document.createElement("a");
  var myNewsLabel = document.createTextNode("Novidades");
  a.href  = '#';
  a.onclick = afterNewsSelected;
  a.appendChild(myNewsLabel);
  myDiv.appendChild(a);

}

/**
 * Funcao que constroi a area de pesquisa de "n" items por editora.
 */
function createBookLimitArea(){
  var myDiv = document.getElementById('arqsiWidgetBookLimit_div');  

  var myTxtBox = document.createElement("input");
  myTxtBox.id = "arqsiWidgetBooksLimit";
  myTxtBox.type="text";
  myTxtBox.name="limitTxt";
  myTxtBox.maxLength="2";  
  myTxtBox.style.width = "20px";
  // para o IE
  myTxtBox.value="10";
  // para os restantes
  myTxtBox.setAttribute("value","10");

  myTxtBox.onchange=validateTextLimit;

  var myNBooksBtn = document.createElement('input');
  myNBooksBtn.type = "button";
  myNBooksBtn.id = "arqsiWidgetNBooks";
  myNBooksBtn.name = "arqsiWidgetNBooks";
  myNBooksBtn.onclick = afterNBooksSelected;
  // para o IE
  myNBooksBtn.value="º itens por Editora";
  // para os restantes
  myNBooksBtn.setAttribute("value","º itens por Editora");

  myDiv.appendChild(myTxtBox);  
  myDiv.appendChild(myNBooksBtn);

}

/**
 * Funcao que constroi a area de pesquisa de livros por texto.
 */
function createBookSearchArea(){
  var myDiv = document.getElementById('arqsiWidgetBookSearch_div');  

  var myTxtBox = document.createElement("input");
  myTxtBox.id = "arqsiWidgetBooksSearch";
  myTxtBox.type="text";
  myTxtBox.name="searchtxt";
  myTxtBox.maxLength="100";  
  myTxtBox.style.width = "400px";

  var mySearchBtn = document.createElement('input');
  mySearchBtn.type = "button";
  mySearchBtn.id = "arqsiWidgetSearch";
  mySearchBtn.name = "arqsiWidgetSearch";
  mySearchBtn.onclick = afterSearchSelected;
  // para o IE
  mySearchBtn.value="Pesquisar livros por título";
  // para os restantes
  mySearchBtn.setAttribute("value","Pesquisar livros por título");

  myDiv.appendChild(myTxtBox);
  myDiv.appendChild(document.createElement("br"));
  myDiv.appendChild(mySearchBtn);

}

/**
 * Funcao que carrega os elementos para a select box de categorias.
 */
function createSelectCategorias(list) {
  var myDiv = document.getElementById('arqsiWidgetCategoriasSelect_div');

  myDiv.innerHTML += '<select id="arqsiWidgetCategories" onchange="afterCategorySelected();"></select>';
  var select = document.getElementById('arqsiWidgetCategories');    
  var optBoogie = document.createElement('option');
  var txtBoogie = document.createTextNode("Escolha uma categoria...");
  optBoogie.value = "-1";
  optBoogie.appendChild(txtBoogie);
  select.appendChild(optBoogie);
  
  var size = list.length;
  for(var i = 0; i < size; i++) {
    var value = list[i].childNodes[0].nodeValue;      
    var opt = document.createElement('option');
    var text = document.createTextNode(value);
    opt.value = value;
    opt.appendChild(text);
    select.appendChild(opt);
  }
}

/**
 * Funções que controlam a paginação sobre  Tabela de Resultados
 */
function changeRecPag() {
  var txtPag = document.getElementById('arqsiWidgetPaging');
  if (isNaN(txtPag.value) || txtPag.value==""){
    recPag = 5;  
  }else{
    recPag = parseInt(txtPag.value);  
  }
  createBookTable();
}
function decPaging(){
  currRec = currRec - recPag;
  if (currRec < 0){
    currRec = 0;
  }
  createBookTable();
}
function incPaging(){
  if (currRec + recPag < bookList.length){
    currRec += recPag;
  }
  createBookTable();
}

/**
 * Funcao que elimina a div com a listagem de Livros
 */
function deleteBookTable() {
  div = document.getElementById('arqsiWidget');
  oldDiv = document.getElementById("arqsiWidgetBookList_div");
  if (oldDiv) div.removeChild(oldDiv);
}

/**
 * Funcao que elimina a div com a listagem de Livros
 */
function deleteUserFeed() {
  div = document.getElementById('arqsiWidget');
  oldDiv = document.getElementById("arqsiWidgetUserFeed_div");
  if (oldDiv) div.removeChild(oldDiv);
}

/**
 * Funcao que mostra um xml resultado de uma pesquisa numa tabela de livros
 */
function createBookTable() {

  deleteBookInfo();
  deleteBookTable();
  deleteUserFeed();

  var div = document.getElementById('arqsiWidget');
  var myTableDiv = document.createElement('div');
  myTableDiv.id = "arqsiWidgetBookList_div";
  div.appendChild(myTableDiv);

  var strInnerHTML = "<br/><table id='arqsiWidgetDados'><thead><tr>";
  strInnerHTML += "<th width='70'></th>";
  strInnerHTML += "<th width='400'>Titulo</th>";
  strInnerHTML += "<th width='70'>Editora</th>";
  strInnerHTML += "<th width='90'></th>";
  strInnerHTML += "</tr></thead><tbody>";
  var j=0;
  for (var i = currRec; j < recPag && i < bookList.length; i++, j++) {    
    var book = bookList[i];
    var titulo = book.getElementsByTagName("title")[0].childNodes[0].nodeValue;      
    var editora = book.getElementsByTagName("editora")[0].childNodes[0].nodeValue;      
    var novidade = book.getElementsByTagName("news")[0].childNodes[0].nodeValue == "sim";      
    var link = "<a href='#' onClick=afterBookInfoSelected("+i+")>+ info</a>";
    strInnerHTML += '<tr>';
    strInnerHTML += '<td align="right">'
    if (novidade) strInnerHTML += '<img src="' + HOST + 'images/new.gif" height="14"/>';
    strInnerHTML += '</td>' 
    strInnerHTML += '<td>' + titulo + '</td>';
    strInnerHTML += '<td>' + editora + '</td>';
    strInnerHTML += '<td>' + link + '</td>';
    strInnerHTML += '</tr>';
  }
  strInnerHTML += "</tbody></table>";  
  myTableDiv.innerHTML = strInnerHTML;
  //Nav Buttons
  strInnerHTML = "<table id='arqsiWidgetNav' border='0'><thead><tr>";
  strInnerHTML += "<th width='300'></th>";
  strInnerHTML += "<th width='335'></th>";
  strInnerHTML += "</tr></thead><tbody>";
  strInnerHTML += '<tr>';
  strInnerHTML += ' <td width="300" ><div id="arqsiWidgetBookListNav1_div"></div></td>';
  strInnerHTML += ' <td width="335" ><div id="arqsiWidgetBookListNav2_div"></div></td>';
  strInnerHTML += '<tr>';
  myTableDiv.innerHTML += strInnerHTML;

  var myNavDiv = document.getElementById('arqsiWidgetBookListNav1_div');
  myNavDiv.innerHTML = "";
  myNavDiv.className = "navleft";
  myNavDiv.innerHTML += '<p>Visualizar <input id="arqsiWidgetPaging" name="paging" type="text" value="' + recPag + '" maxlength="2" style="width:20px" onChange="changeRecPag()"/> registos por página </p>';
  var myNavDiv2 = document.getElementById('arqsiWidgetBookListNav2_div');
  myNavDiv2.className = "navleft";
  myNavDiv2.innerHTML = "";
  myNavDiv2.innerHTML += '<p>';
  if (currRec==0){
    myNavDiv2.innerHTML += "<img src='" + HOST + "images/left_dis.gif' height='16' width='16' style='border:0;'/>";
  }else{
    myNavDiv2.innerHTML += "<a href='#' onClick=decPaging()><img src='" + HOST + "images/left.gif' height='16' width='16' style='border:0;'/></a>";
  }
  var recShown = parseInt(currRec + recPag);
  if (recShown > bookList.length){
    recShown = parseInt(bookList.length);
  }
  myNavDiv2.innerHTML += '&nbsp' + recShown + '/' + bookList.length + '&nbsp';
  if (currRec + recPag >= bookList.length){
    myNavDiv2.innerHTML += "<img src='" + HOST + "images/right_dis.gif' height='16' width='16' style='border:0;'/>&nbsp";
  }else{
    myNavDiv2.innerHTML += "<a href='#' onClick=incPaging()><img src='" + HOST + "images/right.gif' height='16' width='16' style='border:0;'/></a>&nbsp";
  }
  myNavDiv2.innerHTML += '</p>';

}

/**
 * Funcao que elimina a div com a info do livro
 */
function deleteBookInfo() {
  var div = document.getElementById('arqsiWidget');
  var oldDiv = document.getElementById("arqsiWidgetBookInfo_div");
  if (oldDiv) div.removeChild(oldDiv);
}

/**
 * Funcao que mostra uma div com a info do livro
 */
function createBookInfo(book) {

  deleteBookInfo();

  var myBookDiv = document.createElement('div');
  myBookDiv.id = "arqsiWidgetBookInfo_div";
  
  var closeA = document.createElement("a");
  closeA.href = "#";
  closeA.onclick = deleteBookInfo;
  var closePic = document.createElement("img");
  closePic.className = "infoClose";
  closePic.src = "images/bot_close.png";
  closePic.style.width = 20;
  closePic.style.height = 20;
  closePic.style.border = 0;
  closeA.appendChild(closePic);
  myBookDiv.appendChild(closeA);

  var title = document.createElement("p");
  title.className = "bookTitle";
  title.innerHTML = book.getElementsByTagName("title")[0].childNodes[0].nodeValue;
  myBookDiv.appendChild(title);
 
  if (book.getElementsByTagName("picture")[0].childNodes[0].nodeValue!=""){
    var bookPic = document.createElement("img");
    bookPic.className = "bookPic";
    bookPic.src  = book.getElementsByTagName("picture")[0].childNodes[0].nodeValue;
    bookPic.style.width = 100;
    bookPic.style.border = 0;
    bookPic.align = "left";
    myBookDiv.appendChild(bookPic);
  }  

  var author = document.createElement("p");
  author.className = "bookAuthor";
  author.innerHTML = "Autor: "+book.getElementsByTagName("author")[0].childNodes[0].nodeValue;
  myBookDiv.appendChild(author);
 
  var editora = document.createElement("p");
  editora.className = "bookEditora";
  editora.innerHTML = "Editora: "+book.getElementsByTagName("editora")[0].childNodes[0].nodeValue;
  myBookDiv.appendChild(editora);

  var category = document.createElement("p");
  category.className = "bookCategory";
  category.innerHTML = "Categoria: "+book.getElementsByTagName("category")[0].childNodes[0].nodeValue;
  myBookDiv.appendChild(category);

  var lblIsbn = document.createElement("p");
  lblIsbn.className = "bookIsbn";
  lblIsbn.innerHTML = "ISBN: "+book.getElementsByTagName("isbn")[0].childNodes[0].nodeValue;
  ;
  myBookDiv.appendChild(lblIsbn);

  var lblAno = document.createElement("p");
  lblAno.className = "bookPublicacao";
  lblAno.innerHTML = "Ano Publicacão: "+book.getElementsByTagName("publicacao")[0].childNodes[0].nodeValue;
  myBookDiv.appendChild(lblAno);

  var description = document.createElement("p");
  description.className = "bookDescription";
  description.innerHTML = book.getElementsByTagName("description")[0].childNodes[0].nodeValue;
  myBookDiv.appendChild(description);

  div.appendChild(myBookDiv);
}

/**
 *Função que ocntrola o input da caixa de texto de n livros por editora
 */
function validateTextLimit() {
  var myTxtBox = document.getElementById('arqsiWidgetBooksLimit')
  var erro = false;
  if (isNaN(myTxtBox.value)) {
    erro=true;
  } else {
    var i=0;
    i=myTxtBox.value;
    if (i<=0){
      erro=true;
    }
  }
  if (erro){
    alert('Valor introduzido é inválido para o limite. Introduza um valor entre 1 e 99.');
    // para o IE
    myTxtBox.value="10";
    // para os restantes
    myTxtBox.setAttribute("value","10");
  }
}

/**
 *Função que ocntrola o input da caixa de texto de pesquisa por titulo
 */
function validateTextSearch() {
  var myTxtBox = document.getElementById('arqsiWidgetBooksSearch')
  var str = myTxtBox.value;
  if (str.replace(" ", "") == "") {
    alert('Introduza uma expressão para pesquisa por título.');
    return false;
  }
  return true;
}
/**
 * Funcao que serve para fornecer informação de status ao utilizador
 * msg = the message to show
 * level = 0,1,2,3 (normal,good,warning,severe)
 */
function sendUserFeedBack(msg, level){
  var div = document.getElementById('arqsiWidget');

  //Delete previous
  deleteUserFeed();

  //Build new one
  var myDiv = document.createElement('div');
  switch (level){
    case 1 :
      myDiv.className = "feedbackgood";
      break;
    case 2 :
      myDiv.className = "feedbackwarning";
      break;
    case 3 :
      myDiv.className = "feedbacksevere";
      break;      
  }
  myDiv.id = "arqsiWidgetUserFeed_div";
  myDiv.innerHTML = msg;

  div.appendChild(myDiv);  

}
    
/**
 * Funções AJAX
 */

function createXmlHttpRequest() {
  var xmlHttp = null;
  
  if (window.XMLHttpRequest)
  {// code for IE7+, Firefox, Chrome, Opera, Safari
    xmlHttp = new XMLHttpRequest();
  }
  else if (window.ActiveXObject)
  {// code for IE6, IE5
    xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
  }
  
  return xmlHttp;
}

function sendAjaxRequest(method, url, handler, args) {
  var xmlHttp = null;
  xmlHttp = createXmlHttpRequest();
  
  if (!args){
    args = null;
  }
  
  if (xmlHttp != null) {
    switch(method.toUpperCase()) {
      case "POST" :
        xmlHttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        break;
      case "GET":
        if (args!=null) {
          url = url + "?" + args.replace("#", "%23");
        }        
        args = null;
        break;
    }
    
    xmlHttp.open(method, encodeURI(url), true);
    xmlHttp.onreadystatechange = function() {
      handler(xmlHttp);
    };
    
    xmlHttp.send(args);
  }
}

function parseResponse(requestXmlHttpObj) {
  var responseObj = null;
  var responseType = requestXmlHttpObj.responseType;

  if (responseType != '') {
    responseObj = requestXmlHttpObj.responseXML;      
  } else {
    if (window.DOMParser)
    {
      parser = new DOMParser();
      responseObj = parser.parseFromString(requestXmlHttpObj.responseText,"text/xml");
    }
    else // Internet Explorer
    {
      responseObj = new ActiveXObject("Microsoft.XMLDOM");
      responseObj.async = "false";
      responseObj.loadXML(requestXmlHttpObj.responseText);
    }       
  }
    
  return responseObj;
}

function afterGetCategoriesComplete(requestXmlHttpObj) {
  if(requestXmlHttpObj.readyState==REQUEST_COMPLETED && requestXmlHttpObj.status == HTTP_STATUS_OK) {
    var list = new Array();
    var responseObj = parseResponse(requestXmlHttpObj);
    list = responseObj.getElementsByTagName('categoria');
    if (list.length > 0) {
      createSelectCategorias(list);
    } else {
      sendUserFeedBack("Não foi possível obter uma listagem das categorias disponíveis.", 3);
    }    
  }
}

function afterNewsSelected() {
  sendAjaxRequest('GET', servicos.livros, afterGetLivrosCompleted, 'news=1');
}

function afterNBooksSelected() {
  var myNumber = document.getElementById('arqsiWidgetBooksLimit');
  sendAjaxRequest('GET', servicos.livros, afterGetLivrosCompleted, 'numero='+myNumber.value);
}

function afterSearchSelected() {
  if (validateTextSearch()) {
    var mySearch = document.getElementById('arqsiWidgetBooksSearch');
    sendAjaxRequest('GET', 'livros.php', afterGetLivrosCompleted, 'title='+mySearch.value);
  }
}

function afterCategorySelected() {
  var selectCategories = document.getElementById('arqsiWidgetCategories');
  if (selectCategories.tagName == 'SELECT') {
    var categoriaSeleccionada = selectCategories.options[selectCategories.selectedIndex].value;
    if (categoriaSeleccionada != "-1"){
      sendAjaxRequest('GET', servicos.livros, afterGetLivrosCompleted, 'categoria='+categoriaSeleccionada);
    }
  }
}

function afterGetLivrosCompleted(requestXmlHttpObj) {
  if(requestXmlHttpObj.readyState==REQUEST_COMPLETED && requestXmlHttpObj.status == HTTP_STATUS_OK) {
    bookList = new Array();
    var responseObj = parseResponse(requestXmlHttpObj);
    bookList = responseObj.getElementsByTagName('book');
    
    if (bookList.length > 0) {
      currRec=0;
      createBookTable();
    } else {
      deleteBookInfo();
      deleteBookTable();
      sendUserFeedBack("Sem registos...", 2)
    }    
  }
}

function afterBookInfoSelected(rec) {
  var book = bookList[rec];
  var titulo = book.getElementsByTagName("title")[0].childNodes[0].nodeValue;      
  var editora = book.getElementsByTagName("editora")[0].childNodes[0].nodeValue;      
  var isbn = book.getElementsByTagName("isbn")[0].childNodes[0].nodeValue;      
  sendAjaxRequest('GET', servicos.info, afterGetBookInfoCompleted, 'titulo='+titulo+'&editora='+editora+'&isbn='+isbn);
}

function afterGetBookInfoCompleted(requestXmlHttpObj) {
  if(requestXmlHttpObj.readyState==REQUEST_COMPLETED && requestXmlHttpObj.status == HTTP_STATUS_OK) {
    var responseObj = parseResponse(requestXmlHttpObj);
    bookInfo = responseObj.getElementsByTagName('info');
    if (bookInfo.length > 0){
      createBookInfo(responseObj);
    }else{
      deleteBookInfo();
    }
  }
}
