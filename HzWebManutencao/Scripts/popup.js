function popup(pagina, largura, altura) {
	//pega a resolução do visitante
	w = screen.width;
	h = screen.height;

	//divide a resolução por 2, obtendo o centro do monitor
	meio_w = w / 2;
	meio_h = h / 2;

	//diminui o valor da metade da resolução pelo tamanho da janela, fazendo com q ela fique centralizada
	altura2 = altura / 2;
	largura2 = largura / 2;
	meio1 = meio_h - altura2;
	meio2 = meio_w - largura2;

	//abre a nova janela, já com a sua devida posição
	//window.open(pagina, '', "status=no , scrollbars=no ,width=800, height=600 , top=0 , left=0");
	window.open(pagina, '', 'height=' + altura + ', width=' + largura + ', top=' + meio1 + ', left=' + meio2 + ',scrollbars=1,menubar=0');
}
