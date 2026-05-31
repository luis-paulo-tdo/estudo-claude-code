# Curso de Claude Code: Criando sua primeira Aplicação

## 1. Conhecendo o Claude Code

### 1.1. Apresentação
- O curso ensinará como criar uma aplicação do zero utilizando o Claude Code.
- Não é preciso saber programar para usar o Claude Code no desenvolvimento.

### 1.2. O que é Claude Code?
- Antigamente, o processo de desenvolvimento de um software era burocrático e trabalhoso.
- O desenvolvimento demandava conhecimento da linguagem, do ambiente e levava dias.
- Dentre as opções: Contratar algum Dev, pedir ajuda a um amigo ou utilizar No-Code.
- Com o lançamento do Chat GPT, conseguimos conversar com um computador para nos ajudar.
- Dentre outras funções, este computador pode nos ajudar a construir uma aplicação.
- Através dos Chatbots, conseguimos trechos de código e orientações sobre a construção.
- Os Chatbots então evoluíram para participarem da construção dos códigos e protótipos.
- Ferramentas como Lovable, Cursor e Copilot são ferramentas fáceis, mas limitadas.
- Para maximizar o uso delas, ainda é necessário um conhecimento em desenvolvimento.
- Então, surgem os Agentes de IA, que participam ativamente do desenvolvimento. 
- O Claude Code permite pessoas mais leigas a conseguirem criar aplicações.
- Ele permite a leitura de arquivos no projeto, e a criação e edição de arquivos.
- Ele age como um programador freelancer, onde pedimos algo e ele constrói.
- A diferença agora é saber o que queremos pedir, especificando os detalhes.
- Com isto, nos tornamos os chefes do projeto, ditando o que o Claude irá fazer.
- É preciso pensar com clareza, planejar com intenão e comunicar com precisão.

### 1.3. O Loop do Claude Code
- A forma comum de uso da IA é digitando qualquer coisa e recebendo uma resposta.
- Geralmente, os Chatbots retornam trechos de código soltos e você deve usá-los.
- Os Agentes, por outro lado, se preocupam com todo o contexto do projeto.
- Ele vai entender, buscar contexto, agir, verificar, corrigir e entregar.
- O próprio Claude irá atuar em todo o ciclo de desenvolvimento da aplicação.
- Para que isto funcione, precisamos também fazer parte deste ciclo ativamente.
- Antes, pedíamos um código, o Chatbot nos devolvia e torciamos para funcionar.
- Hoje, a gente descreve o que queremos, vê o que o Agente faz e vai ajustando.

### 1.4. Instalando e Rodando
- O download do Agente é requer no mínimo o Plano Pro e é feito no site do Claude.
- Uma vez instalado, deve-se abrir o PowerShell como admin e rodar o `claude`.
- Tenha cuidado ao utilizar o Claude com projetos de terceiros e não confiáveis.

## 2. Planejando uma Aplicação

### 2.1. Entendendo o Problema
- Antigamente, os geradores de código tinham problemas com contexto que eram muito grandes.
- Era preferível fazer a quebra do problema em partes e ir gerando os códigos aos poucos.
- O Claude Code prefere construir muitas coisas de uma vez desde que o prompt esteja claro.
- Definir em um arquivo os detalhes técnicos e de negócio essenciais é meio caminho andado.
- Através deste arquivo, o Claude consegue nortear o desenvolvimento dentro do contexto.
- A primeira coisa que fazemos, é pedir ao Claude ajuda na construção de um arquivo.
- Neste primeiro prompt, descrevemos o que é e os objetivos da aplicação desejada.
- Além disso, pedimos para que o Claude faça uma entrevista para definir as premissas.
- Todas estas informações deverão ser salvas em um arquivo que ajudará no desenvolvimento.
- Premissas iniciais: Público-alvo, diferencial, modelo de negócio, MVP e tecnologias.
- Devemos evitar muitas lacunas, pois o Agente pode preenchê-las com coisas sem sentido.

### 2.2. Alternativas ao Terminal
- Após a entrevista, o Claude gera um arquivo de Markdown contendo todas as premissas.
- Neste arquivo, temos a visão geral do projeto, o público-alvo e os diferenciais.
- O arquivo fala sobre MVP, funcionalidades essenciais, design e stack utilizadas.
- Especifica também as decisões arquiteturais orientadoras e itens fora do escopo.
- Também, lista os princípios do projeto e os primeiros passos a serem seguidos.
- É possível continuar o trabalho utilizando o VS Code em vez do PowerShell.
- Para isto, precisamos baixar a IDE e configurar o plugin do Claude Code.
- O plugin nos permite já iniciar o Claude dentro do diretório que abrimos.