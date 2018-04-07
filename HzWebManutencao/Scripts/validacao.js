var vr;

var VAL_CPF = 1
var VAL_RG = 2
var VAL_DATA = 3
var VAL_CEP = 4
var VAL_NUMERO = 5
var VAL_TELEFONE = 6 
var VAL_MOEDA = 8
var VAL_CNPJ = 9
var VAL_PIS_PASEP = 10

Array.prototype.indexOf =      
    function indexOf(value) {
				for (x = 0; x<this.length; x++) 
				{
					if(this[x] == value) return x;

				}
					return -1;
}

function desabilitaCTRL(event) {
	if (window.event) {
		if (window.event.ctrlKey)
			window.event.returnValue = false;
	} else {
			event.preventDefault();
	}
}

function FormClear(frm)
{
	for (var j = 0; j < frm.all.length; j++)
	{
		switch (frm.all[j].tagName)
		{
			case ("INPUT")    : 
								switch (frm.all[j].type)
								{
									case ("text")	 :
									case ("password"):
									case ("file")	 :
														frm.all[j].value = "";
														break;
									case ("checkbox"):
									case ("radio")	 :
														frm.all[j].checked = false;
														break;
												
								}
								break;
								
			case ("SELECT")   : 
								if (frm.all[j].options.length > 0)
								{
									frm.all[j].selectedIndex = 0;
								}
								break;
								
			case ("TEXTAREA") : 
								frm.all[j].value = "";
								break;														
		}
	}
}

function validate(pai) {

            var cont = pai.all;

            var validado = new Array();

            for (i=0;i<cont.length;i++) {

                     if (cont[i].getAttribute('validate') == 'TXT') {

                                   if (Trim(cont[i].value) == '') {

                                               cont[i].style.backgroundColor='#D3D05F';

                                               validado.push(false);

                                   } else {

                                               cont[i].style.backgroundColor='';

                                               validado.push(true);

                                   }

                        }

                        if (cont[i].getAttribute('validate') == 'INT') {

                                   if (Trim(cont[i].value) == '') {

                                               cont[i].style.backgroundColor='#D3D05F';

                                               validado.push(false);

                                   } else {

                                               if (parseInt(cont[i].value) == 0) {

                                                           cont[i].style.backgroundColor='#D3D05F';

                                                           validado.push(false);

                                               } else {

                                                           cont[i].style.backgroundColor='';

                                                           validado.push(true);

                                               }

                                   }

                        }

            }

            if (validado.indexOf(false) > -1) {
                        top.dlg.setTipo(0);
                        top.dlg.setContent("Campo(os) Obrigatório (os)!");
                        top.dlg.on();
            }

            return validado.indexOf(false) < 0;
     }


function FormataDado(campo, tamMax, pos, event)
{
	if (window.event) 
	{
	  var src = window.event.srcElement;
	  var key = window.event.keyCode;
	}
	else 
	{
	  var src = event.target;
	  var key = event.which;
	}
	
	var tecla = String.fromCharCode(key).toUpperCase().charCodeAt(0);

	vr = '';

	vr = src.value;
	var tam = vr.length ;

	if (tam < tamMax && tecla != 8)
		tam = vr.length + 1;
	
	if (tecla == 8)
		tam = tam - 1 ;

	if (tecla == 0 || tecla == 8 || tecla == 9 || (tecla == 46 && event.type=='keyup') || tecla >= 48 && tecla <= 57 || tecla >= 65 && tecla <= 73 || tecla >= 96 && tecla <= 105) {
/*
		parametro campo
		4 para campo CEP
		5 para campo numérico
		8 para campo monetario
*/

		switch (campo) {
			case 1: {
				formataCPF(tam, src);
				break; }

			//formata Hora
			case 2: { 
				//alert('asdfasd');
				formataHora(tam, src); 
				break; }

			//formata Data
			case 3: { 
				//alert('asdfasd');
				formataData(tam, src); 
				break; }
				
			//formata CEP
			case 4: { 
				FormataCEP(tam, src); 
				break; }

			//campo numerico
			case 5: { 
				FormataNumero(src, tamMax, event)
				break; 
					}
					
			//formata telefone
			case 6: { 
				formataTelefone(tamMax,pos, src); 
				break; }
			
			//formata valor
			case 8: {
				FormataValor(src, tamMax, key, event); 
				break; }
			
			default: {}
		}
	}
	else {
			if (window.event) 
			{
				window.event.returnValue = false;
			}
			else 
			{
				event.preventDefault();
			}
	
		}
}

function VerificarEmail(strEmail)
{
//	var re = new RegExp(/^[a-zA-Z]+[a-zA-Z0-9_\-\.]+@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/);

	if (strEmail.length == 0)
	{		
		return (false);
	}
	var checkChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
	var checkOK = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@._-";
                var checkStr = strEmail;
                var allValid = true;
		var i;
	        var j;
                var ch;
                var temarroba;
                var temponto;
                var temespecificacao;

                ch = checkStr.charAt(0);

                for (j = 0;  j < checkChar.length;  j++)
		{
                    if (ch == checkChar.charAt(j))
			break;
		}
		if (j == checkChar.length)
                {
			return (false);
                }

                for (i = 0;  i < checkStr.length;  i++)
                {
                  ch = checkStr.charAt(i);
                  for (j = 0;  j < checkOK.length;  j++)
                    if (ch == checkOK.charAt(j))
                      break;
                  if (j == checkOK.length)
                  {
                     allValid = false;
                     break;
                  }
                }

                if (allValid){
                  allValid = false;
                  temarroba = false;
                  temponto = false;
		  temespecificacao = false;
                  for (i = 0;  i < checkStr.length;  i++)
                  {
                    ch = checkStr.charAt(i);
                    if (ch == "@")
                    {
                        temarroba = true;
                        if ((i != 0) && (i != checkStr.length-1))
                        {
                                allValid = true;
                        }
                        else
                        {
                                allValid = false;
                                break;
                        }
                    }
                    if (temarroba)
                    {
                        if (ch == ".")
                        {
                                temponto = true;        
                        }
                    }
		    if (temponto)
		    {
			if (ch != ".")
			{
				temespecificacao = true;
			}
		    }
                  }
                  if (!(temarroba && temponto && temespecificacao))
                  {
                        allValid = false;
                  }
                }
                if (!allValid)
                {
                  return (false);
                }
		return (true);
//	return re.test(email);
}

function FormataXML(valor)
{
	valor = valor.replace(/</g,'&lt;');
	valor = valor.replace(/>/g,'&gt;');
	valor = valor.replace(/&/g,'&amp;');
	valor = valor.replace(/'/g,'&apos;');
	valor = valor.replace(/"/g,'&quot;');
	
	return valor;

}

function FormataEntrada(event)
{
	if (window.event) 
	{
	  var src = window.event.srcElement;
	  var key = window.event.keyCode;
	}
	else 
	{
	  var src = event.target;
	  var key = event.which;
	}
	
	var tecla = String.fromCharCode(key).toUpperCase().charCodeAt(0);

	if (((tecla == 34 || tecla == 38 || tecla == 60 || tecla == 62)))
	{
		if (window.event) 
		{
			window.event.returnValue = false;
		}
		else 
		{
			event.preventDefault();
		}
		return false;
	}
}

function LimitaTamanho(tam, event)
{

	if (window.event) 
	{
	  var src = window.event.srcElement;
	  var key = window.event.keyCode;
	}
	else 
	{
	  var src = event.target;
	  var key = event.which;
	}
	
	var valor = src.value;
	var tecla = String.fromCharCode(key).toUpperCase().charCodeAt(0);

	if (tecla == 0)
	{
		return true;
	}
	
	
	if (valor.length >= tam)
	{
		if (window.event) 
		{
			window.event.returnValue = false;
		}
		else 
		{
			event.preventDefault();
		}
		return false;
	}
}

function FormataEmail(event)
{
	if (window.event) 
	{
	  var src = window.event.srcElement;
	  var key = window.event.keyCode;
	}
	else 
	{
	  var src = event.target;
	  var key = event.which;
	}
	
	var valor = src.value;
	var tecla = String.fromCharCode(key).toUpperCase().charCodeAt(0);
	
	if ((valor.length == 0) && (tecla == 46))
	{
		if (window.event) 
		{
			window.event.returnValue = false;
		}
		else 
		{
			event.preventDefault();
		}
		return false;
	}
	else
	{
		if ((valor.substr(valor.length-1,1) == '.') && (tecla == 46))
		{
			if (window.event) 
			{
				window.event.returnValue = false;
			}
			else 
			{
				event.preventDefault();
			}
			return false;
		}
	}
	
	if (!(tecla >= 65 && tecla <= 90 || tecla >= 48 && tecla <= 57 || tecla == 46 || tecla == 8 || tecla == 0))
	{
		if (window.event) 
		{
			window.event.returnValue = false;
		}
		else 
		{
			event.preventDefault();
		}
	}
}

function FormataEmail2(event)
{
	if (window.event) 
	{
	  var src = window.event.srcElement;
	  var key = window.event.keyCode;
	}
	else 
	{
	  var src = event.target;
	  var key = event.which;
	}
	
	var valor = src.value;
	var tecla = String.fromCharCode(key).toUpperCase().charCodeAt(0);
	
 	if ((valor.length == 0) && (tecla == 64))
	{
		if (window.event) 
		{
			window.event.returnValue = false;
		}
		else 
		{
			event.preventDefault();
		}
		return false;
	}
	else
	{
		if ((valor.indexOf('@') > -1) && (tecla == 64) || (valor.substr(valor.length-1,1) == '.') && (tecla == 64))
		{
			if (window.event) 
			{
				window.event.returnValue = false;
			}
			else 
			{
				event.preventDefault();
			}
			return false;
		}
	}

	if ((valor.length == 0) && (tecla == 46))
	{
		if (window.event) 
		{
			window.event.returnValue = false;
		}
		else 
		{
			event.preventDefault();
		}
		return false;
	}
	else
	{
		if ((valor.substr(valor.length-1,1) == '.') && (tecla == 46) || (valor.substr(valor.length-1,1) == '@') && (tecla == 46))
		{
			if (window.event) 
			{
				window.event.returnValue = false;
			}
			else 
			{
				event.preventDefault();
			}
			return false;
		}
	}
	
	if (!(tecla >= 65 && tecla <= 90 || tecla >= 48 && tecla <= 57 || tecla == 46 || tecla == 64 || tecla == 95 || tecla == 8 || tecla == 0))
	{
		if (window.event) 
		{
			window.event.returnValue = false;
		}
		else 
		{
			event.preventDefault();
		}
		return false;
	}
}

function FormataNome(event)
{
	if (window.event) 
	{
	  var src = window.event.srcElement;
	  var key = window.event.keyCode;
	}
	else 
	{
	  var src = event.target;
	  var key = event.which;
	}

	var tecla = String.fromCharCode(key).toUpperCase().charCodeAt(0);
	
	if (!(tecla >= 65 && tecla <= 90 || tecla == 32 || tecla >= 192 && tecla <= 221 || tecla == 8 || tecla == 0))
	{
		if (window.event) 
		{
			window.event.returnValue = false;
		}
		else 
		{
			event.preventDefault();
		}
	}
}

function FormataSenha(event)
{
	if (window.event) 
	{
	  var src = window.event.srcElement;
	  var key = window.event.keyCode;
	}
	else 
	{
	  var src = event.target;
	  var key = event.which;
	}
	
	var tecla = String.fromCharCode(key).toUpperCase().charCodeAt(0);
	
	if (!(tecla >= 65 && tecla <= 90 || tecla >= 48 && tecla <= 57 || tecla == 8 || tecla == 0))
	{
		if (window.event) 
		{
			window.event.returnValue = false;
		}
		else 
		{
			event.preventDefault();
		}
	}
}

function FormataIdentidade(event)
{
	if (window.event) 
	{
	  var src = window.event.srcElement;
	  var key = window.event.keyCode;
	}
	else 
	{
	  var src = event.target;
	  var key = event.which;
	}

	var tecla = String.fromCharCode(key).toUpperCase().charCodeAt(0);

	if (!((tecla >= 65 && tecla <= 90) || (tecla >= 48 && tecla <= 57) || tecla == 0 ))
	{
		if (window.event) 
		{
			window.event.returnValue = false;
		}
		else 
		{
			event.preventDefault();
		}
	}
}
	
function formataPIS(tam, src)
{
	if ( tam == 2 ){
		src.value = vr.substr( 0, 1 ) + '.' + vr.substr( 2, tam ); }
	
	if ( tam == 6){
		src.value = vr.substr( 0, 5 ) + '.' + vr.substr( 6, tam ); }
	
	if ( tam == 10){
		src.value = vr.substr( 0, 9 ) + '.' + vr.substr( 10, tam );	}
	
	if ( tam == 14){
		src.value = vr.substr( 0, 13 ) + '-' + vr.substr( 14, tam ); }
}

function formataCPF(tam, src)
{
	if ( tam <= 2 ){
		src.value = vr; }
	
	if ( tam == 4){
		src.value = vr.substr( 0, 3 ) + '.' + vr.substr( 4, tam ); }
	
	if ( tam == 8){
		src.value = vr.substr( 0, 7 ) + '.' + vr.substr( 8, tam );	}
	
	if ( tam == 12){
		src.value = vr.substr( 0, 11 ) + '-' + vr.substr( 12, tam ); }
}
	
function formataRG(tam, src)
{
	if ( tam == 1){
		src.value = vr; }
	
	if ( tam == 2){
		src.value = vr.substr( 0, 1 ) + '.' + vr.substr( 2, tam ); }
	
	if ( tam == 6){
		src.value = vr.substr( 0, 5 ) + '.' + vr.substr( 7, tam );	}
}

function mostraMascara(o)
{
	if (o.value == '')
	{
		o.value = '__/__/____'
	}
	else
	{
		if (o.value == '__/__/____')
		{
			o.value = ''
		}
	}
}

function formataData(tam, src)
{
	if ( tam == 1){
		src.value = vr; }
	
	if ( tam == 3){
		src.value = vr.substr( 0, 2 ) + '/' + vr.substr( 4, tam);	}
	
	if ( tam == 6){
		src.value = vr.substr( 0, 5 ) + '/' + vr.substr( 7, tam);	}
}

function formataHora(tam, src)
{
	if ( tam == 1){
		src.value = vr; }
	
	if ( tam == 3){
		src.value = vr.substr( 0, 2 ) + ':' + vr.substr( 4, tam);	}
}

function formataData2(tam, src)
{
	if ( tam == 1){
		src.value = vr; }
	
	if ( tam == 3){
		src.value = vr.substr(0, 2) + '/' + vr.substr(4);	}
}

function formataCEP(tam, src)
{
	if ( tam == 1){
		src.value = vr; }
	
	if ( tam == 6){
		src.value = vr.substr( 0, 6 ) + '-' + vr.substr( 8, tam );	}
}

function formataTelefone(tam,tamMax,pos,src)
{
	if ( tam == 1){
		src.value = vr; }
	
	if ( tam > pos && tam <= tamMax ){
		vr = vr.replace( "-", "" );
		src.value = vr.substr( 0, tam - pos ) + '-' + vr.substr( tam - pos, tam );	}
}

function formataCNPJ(tam,src)
{
	if ( tam <= 2 ){
		src.value = vr; }
	
	if ( tam == 3){
		src.value = vr.substr( 0, 2 ) + '.' + vr.substr( 3, tam ); }
	
	if ( tam == 7){
		src.value = vr.substr( 0, 6 ) + '.' + vr.substr( 7, tam );	}
	
	if ( tam == 11){
		src.value = vr.substr( 0, 10 ) + '/' + vr.substr( 11, tam );	}
	
	if ( tam == 16){
		src.value = vr.substr( 0, 15 ) + '-' + vr.substr( 16, tam ); }
}

function formatarValor(src)
{
	vr = src.value;

	vr = vr.replace(',', '');
	vr = vr.replace('.', '');
	vr = vr.replace('.', '');
	vr = vr.replace('.', '');
	vr = vr.replace('.', '');
	vr = vr.replace('.', '');
		
	if ( tam <= 2 ){ 
		src.value = vr ; }
	if ( (tam > 2) && (tam <= 5) ){
		src.value = vr.substr(0, tam - 2) + ',' + vr.substr(tam - 2, tam) ; }
	if ( (tam >= 6) && (tam <= 8) ){
		src.value = vr.substr(0, tam - 5) + '.' + vr.substr(tam - 5, 3) + ',' + vr.substr( tam - 2, tam ) ; }
	if ( (tam >= 9) && (tam <= 11) ){
		src.value = vr.substr(0, tam - 8) + '.' + vr.substr(tam - 8, 3) + '.' + vr.substr( tam - 5, 3 ) + ',' + vr.substr( tam - 2, tam ) ; }
	if ( (tam >= 12) && (tam <= 14) ){
		src.value = vr.substr(0, tam - 11) + '.' + vr.substr(tam - 11, 3) + '.' + vr.substr( tam - 8, 3 ) + '.' + vr.substr( tam - 5, 3 ) + ',' + vr.substr( tam - 2, tam ) ; }
	if ( (tam >= 15) && (tam <= 17) ){
		src.value = vr.substr(0, tam - 14) + '.' + vr.substr(tam - 14, 3) + '.' + vr.substr( tam - 11, 3 ) + '.' + vr.substr( tam - 8, 3 ) + '.' + vr.substr( tam - 5, 3 ) + ',' + vr.substr( tam - 2, tam ) ;}
}

function FormataValor(campo, tammax, tecla, event) {
	
	if (document.selection.createRange().text != '' && tecla != 9)
	{
		document.selection.createRange().text = '';
	}
	
	var vr = campo.value;
	vr = vr.replace( /\//g, "" );
	vr = vr.replace( /,/g, "" );
	vr = vr.replace( /\./g, "" );
	var tam = vr.length;
	
	if (tam > parseInt(tammax, 10))
	{
		if (window.event) 
		{
			window.event.returnValue = false;
		}
		else 
		{
			event.preventDefault();
		}
		return;
	}

	if (tecla != 8 && tecla != 46 && event.type != 'keyup'){ tam = vr.length + 1 ; }

	if (tecla == 8){	tam = vr.length ; }
	
	if ( (tecla == 8) || (tecla == 46) || (tecla >= 48 && tecla <= 57) || (tecla >= 96 && tecla <= 105) )
	{
		if ( tam <= 2 ){ 
	 		campo.value = vr ; }
	 	if ( (tam > 2) && (tam <= 5) ){
	 		campo.value = '' + vr.substr( 0, tam - 2 ) + ',' + vr.substr( tam - 2, tam ) ; }
	 	if ( (tam >= 6) && (tam <= 8) ){
	 		campo.value = vr.substr( 0, tam - 5 ) + '.' + vr.substr( tam - 5, 3 ) + ',' + vr.substr( tam - 2, tam ) ; }
	 	if ( (tam >= 9) && (tam <= 11) ){
	 		campo.value = vr.substr( 0, tam - 8 ) + '.' + vr.substr( tam - 8, 3 ) + '.' + vr.substr( tam - 5, 3 ) + ',' + vr.substr( tam - 2, tam ) ; }
	 	if ( (tam >= 12) && (tam <= 14) ){
	 		campo.value = vr.substr( 0, tam - 11 ) + '.' + vr.substr( tam - 11, 3 ) + '.' + vr.substr( tam - 8, 3 ) + '.' + vr.substr( tam - 5, 3 ) + ',' + vr.substr( tam - 2, tam ) ; }
	 	if ( (tam >= 15) && (tam <= 17) ){
	 		campo.value = vr.substr( 0, tam - 14 ) + '.' + vr.substr( tam - 14, 3 ) + '.' + vr.substr( tam - 11, 3 ) + '.' + vr.substr( tam - 8, 3 ) + '.' + vr.substr( tam - 5, 3 ) + ',' + vr.substr( tam - 2, tam ) ;}
	 	if ( (tam >= 18) && (tam <= 20) ){
	 		campo.value = vr.substr( 0, tam - 17 ) + '.' + vr.substr( tam - 17, 3 ) + '.' + vr.substr( 0, tam - 14 ) + '.' + vr.substr( tam - 14, 3 ) + '.' + vr.substr( tam - 11, 3 ) + '.' + vr.substr( tam - 8, 3 ) + '.' + vr.substr( tam - 5, 3 ) + ',' + vr.substr( tam - 2, tam ) ;}
	 	if ( (tam >= 21) && (tam <= 23) ){
	 		campo.value = vr.substr( 0, tam - 20 ) + '.' + vr.substr( tam - 20, 3 ) + '.' + vr.substr( 0, tam - 17 ) + '.' + vr.substr( tam - 17, 3 ) + '.' + vr.substr( 0, tam - 14 ) + '.' + vr.substr( tam - 14, 3 ) + '.' + vr.substr( tam - 11, 3 ) + '.' + vr.substr( tam - 8, 3 ) + '.' + vr.substr( tam - 5, 3 ) + ',' + vr.substr( tam - 2, tam ) ;}
	}
}

function validaCNPJ(NroCNPJ) {
	var dig1=0;
	var dig2=0;
	var x;
	var Mult1 = '543298765432';
	var Mult2 = '6543298765432';
	
	NroCNPJ = NroCNPJ.replace('.','');
	NroCNPJ = NroCNPJ.replace('.','');
	NroCNPJ = NroCNPJ.replace('/','');	
	NroCNPJ = NroCNPJ.replace('-','');
	
	for(x=0; x<=11; x++) 
	{
		dig1 = dig1 +(parseInt(NroCNPJ.slice(x,x+1)) * parseInt(Mult1.slice(x,
		x+1)) ) ;
	}
	
	for(x=0; x<=12; x++) 
	{
		dig2 = dig2 + (parseInt(NroCNPJ.slice(x, x+1)) * parseInt(Mult2.slice(x,x+1)));
	}

	dig1 = (dig1 * 10)%11;
	dig2 = (dig2 * 10)%11;
	
	if (dig1 == 10) {dig1 = 0;}
	if (dig2 == 10) {dig2 = 0;}
	if (dig1 != parseInt(NroCNPJ.slice(12, 13))) 
	{
		return false;
	} 
	else 
	{
		if (dig2 != parseInt(NroCNPJ.slice(13, 14))) 
		{
			return false;
		} 
		else 
		{
			return true;
		}
	}
}

<!--Funções de verificação de CPF válido-->
function verificaCPF(CPF)
{
	var sCPF = CPF;
	
	sCPF = sCPF.replace('.','');
	sCPF = sCPF.replace('.','');
	sCPF = sCPF.replace('-','');
	
	sDigito = sCPF.substring(sCPF.length,sCPF.length-2)
	sNumero = sCPF.substring(0,sCPF.length-2)
	
	sCpf = calculaDigitoMod11(sNumero,2,1)
	
	if (sDigito == sCpf){
		return(true)
	}else{
		return(false); }	
}
	
function calculaDigitoMod11(sValor,iDigSaida,sTipoValidacao)
{
	if (sTipoValidacao == 1) iCod = 12  

	for (t=1;t<=iDigSaida;t++){
		soma = 0
		mult = 2
		for (j=sValor.length;j>0;j--){
			soma = soma + (mult * parseInt(sValor.substring(j,j-1),10))
			mult++
			if (mult > iCod) mult = 2
		}
		soma = (soma * 10) % 11
		if (soma == 10) sValor = sValor + "0"
		else sValor = sValor + soma
	}
	return sValor.substring(sValor.length-iDigSaida,sValor.length)
}


function verificaPIS(PIS)
{
	var sPIS = PIS;
		
	sPIS = sPIS.replace('.','');
	sPIS = sPIS.replace('.','');
	sPIS = sPIS.replace('.','');	
	sPIS = sPIS.replace('-','');
	
	var sDigito = sPIS.substring(sPIS.length,sPIS.length-1)
	var sNumero = sPIS.substring(0,sPIS.length-1)
		
	var produto = parseInt(sNumero.substr(0,1)) * 3 +
				  parseInt(sNumero.substr(1,1)) * 2 +
				  parseInt(sNumero.substr(2,1)) * 9 +  
				  parseInt(sNumero.substr(3,1)) * 8 +  
				  parseInt(sNumero.substr(4,1)) * 7 +  
				  parseInt(sNumero.substr(5,1)) * 6 +  
				  parseInt(sNumero.substr(6,1)) * 5 +  
				  parseInt(sNumero.substr(7,1)) * 4 +  
				  parseInt(sNumero.substr(8,1)) * 3 +
				  parseInt(sNumero.substr(9,1)) * 2;
					  
	var res = produto % 11;
	var sub = 11 - res;
	sub = (sub == 10) || (sub == 11) ? 0 : sub;
					
	return (sub == sDigito);
}	

function verificaNIS(numero)
{
	var Mlt = new Array(8, 9, 2, 3, 4, 5, 6, 7, 8, 9, 0);	// Multiplicador
	var NrNIS = new Array(10);													// Nis
	var Prd = new Array(11);														// Produto
	var Digito;																					// Digito
	var Soma = 0;
	var A;

	if (!IsNumeric(numero))
		return false;	// Tipo de dado inválido.

	if (numero.length < 10 || numero.length > 11)
		return false;		// Quantidade de dígitos inválidos.

	// Captura os numeros
	var Converte = String();
	Converte = numero;

	for (A = 1; A <= 10; A++) {
	   NrNIS[A-1] = parseInt(Converte.substr(A-1, 1), 0);
	}

	// Calcula o digito verificador
	for (A = 0; A < NrNIS.length; A++) {
	   Prd[A] = NrNIS[A] * Mlt[A];
	   Soma += Prd[A];
	}

	divido = parseInt(Soma / 11, 0);
	multiplico = divido * 11;

	Digito = Soma - multiplico;

	var DigitoInformado = parseInt(Converte.substr(Converte.length - 1, 1), 0);

	if (DigitoInformado > 9)
		DigitoInformado = 0;

	if (Digito == DigitoInformado)
		return true;
	else
		return false;
}


function IsNumeric(valor)
{
	var i;
	var sCPF = valor;
	
	sCPF = sCPF.replace('.','');
	sCPF = sCPF.replace('.','');
	sCPF = sCPF.replace('/','');
	sCPF = sCPF.replace('-','');
	
	for   (i = 0; i < sCPF.length; i++){
		if (sCPF.charAt(i) < '0' || sCPF.charAt(i) > '9'){
			return false; }
	}
	return true;
}
	
function fSoCaracter(_string,carac)
{
	for(i=0;i<_string.length;i++){
		if(_string.charAt(i)!=carac)
			return false;	
	}
	return true;
}



<!--Verifica Email.-->

function EmailCorreto (conteudo)
{
	Valor = false

	for (Count=0; Count<conteudo.length; Count++) {
		ch = conteudo.substring (Count, Count+1);
		if (ch=='@' && ((conteudo.length - (Count+1)) >0 )) {
			// Se existe outro @ no mesmo endereço de email, retorna falso
			if ( Valor ) 	
				return (false);
			else
				Valor = true;
		}		
	} // Fim for
	return (Valor);
}

<!--Verifica data válida-->
function validaDataV(data)
{
	var strDataConstituicao = '';
	var vetData = new Array();
	var iDia, iMes, iAno
	
	if (data.length != 10)
	{
		return false;
	}
			
	strDataConstituicao = new String(data);
	vetData = strDataConstituicao.split('/');

	if (vetData.length < 3)
	{
		return false;
	}
	
	iDia = Number(vetData[0], 0);
	iMes = Number(vetData[1], 0);
	iAno = Number(vetData[2], 0);
	
	if((iMes == 1)||(iMes == 3)||(iMes == 5)||(iMes == 7)||(iMes == 8)||(iMes == 10)||(iMes == 12)){
		if(iDia > 31){
			return false;
		}else{
			return true;
		}
	}else{
		if((iMes == 4)||(iMes == 6)||(iMes == 9)||(iMes == 11)){
			if(iDia > 30){
				return false;
			}else{
				return true;
			}
		}else{
			if(iMes == 2){
				var modAnoBisexto = iAno % 4;
						
				if(modAnoBisexto == 0){
					if(iDia > 29){
						return false;
					}else{
						return true;
					}
				}else{
					if(iDia > 28){
						return false;
					}else{
						return true;
					}
				}
			}else{
				return false;
			}
		}
	}
}

function verficaDataIF(dtInicial, dtvar)
{
	var sDataInicial = dtInicial;
	var sDatavar = dtvar;
	
	var iDiaInicial = '';
	var iDiavar = '';
	var iMesInicial = '';
	var iMesvar = '';
	var iAnoInicial = '';
	var iAnovar = '';
}

function verificarDTNascimento(data, tipo, dataAtual)
{
	if (!validaDataV(data))
	{
		return false;
	}
	
	var da = new Date(data);
	var di = new Date(dataAtual);
	
	dataAtual = dataAtual.split('/');
	var diaAtual = dataAtual[0];
	var mesAtual = dataAtual[1];
	var anoAtual = dataAtual[2];
		
	data = data.split('/');
	var dia = data[0];
	var mes = data[1];
	var ano = data[2];
		
	if((ano > anoAtual)){
		return false;
	}else{
		if(ano == anoAtual)
		{
			if (tipo == 1)
			{
				return false;
			}
			
			if(mes > mesAtual){
				return false;
			}else{
				if(mes == mesAtual){
					if(dia > diaAtual){
						return false;
					}else
						return true;
				}else
					return true;
			}
		}
		else
		{
			switch (tipo)
			{
				case 0:
						if(!((ano <= (anoAtual - 10)) && (ano > (anoAtual - 110))))
						{
							return false;
						}
						else
						{
						return true;
						}
						break;
				case 1:
						if(!((ano <= (anoAtual - 10)) && (ano > (anoAtual - 18))))
						{
							return false;
						}
						else
						{
							return true;
						}
						break;
		}
		}
		return true;
	}
}

function verificarDT(data, dataAtual)
{
	if (!validaDataV(data))
	{
		return false;
	}
	
	data = data.split('/');
	var dia = data[0];
	var mes = data[1];
	var ano = data[2];
	
	dataAtual = dataAtual.split('/');
	var diaAtual = dataAtual[0];
	var mesAtual = dataAtual[1];
	var anoAtual = dataAtual[2];	
		
	if(ano <= (anoAtual - 110))
	{
		return false;
	}else
	{
		return true;
	}
}

function Trim(s) 
{
  while ((s.substring(0,1) == ' ') || (s.substring(0,1) == '\n') || (s.substring(0,1) == '\r'))
  {
    s = s.substring(1,s.length);
  }

  while ((s.substring(s.length-1,s.length) == ' ') || (s.substring(s.length-1,s.length) == '\n') || (s.substring(s.length-1,s.length) == '\r'))
  {
    s = s.substring(0,s.length-1);
  }
  return s;
}

function FormataNumero(campo, tamMax, event) 
{
	if (window.event) 
	{
	  var src = window.event.srcElement;
	  var key = window.event.keyCode;
	}
	else 
	{
	  var src = event.target;
	  var key = event.which;
	}
	
	var tecla = String.fromCharCode(key).toUpperCase().charCodeAt(0);

	vr = campo.value;

	if (vr.length > parseInt(tamMax, 10))
	{
		if (window.event) 
		{
			window.event.returnValue = false;
		}
		else 
		{
			event.preventDefault();
		}
		return;
	}	
	if ( (tecla == 8) || (tecla == 9) || (tecla == 46) || (tecla >= 48 && tecla <= 57) || (tecla >= 97 && tecla <= 105) ) 
	{
		//faz nada. 
	}
	else if (tecla == 44) {
		if (vr.indexOf(',') == -1) 
		{
			// faz nada de novo.
		}
		else 
		{
			if (window.event) 
			{
				window.event.returnValue = false;
			}
			else 
			{
				event.preventDefault();
			}
		}
	}
	else 
	{
		if (window.event) 
		{
			window.event.returnValue = false;
		}
		else 
		{
			event.preventDefault();
		}
	}
}

function FormataMaxLength(tammax, event)
{
	if (window.event) 
	{
		var src = window.event.srcElement;
		var key = window.event.keyCode;
	}
	else 
	{
		var src = event.target;
		var key = event.which;
	}
    var vr = src.value;
     
    if (vr.length >= tammax && key!=8 && key!=9 && key!=16 && key!=37 && key!=38 && key!=39 && key!=40 && key!=46)
    {
		if (window.event)
		{
			window.event.returnValue = false;
		}
		else 
		{
			event.preventDefault();
		}
		src.value = vr.substr(0, tammax);
    }
}

function isAnyChecked(campo)
{
	intNumCampos = 0;
	while (typeof(eval(campo + "_" + intNumCampos)) == 'object')
	{
		if (eval(campo + "_" + intNumCampos).checked)
			return true;
		intNumCampos++;
	}
	return false;
}	