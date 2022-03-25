# JslWeb
Projeto para consumo da API (JslApi).

Tempo de Desenvolvimento deste projeto: Em torno de 3 horas.

Principais dificuldades:
* Realizar na mão as tratativas dos controllers para consumir a API.
* Tratar corretamente a Serialização dos dados ao tentar gravar dados (após entender a melhor forma, ficou mais fácil).

Observações interessantes:
* Uma parte considerável do tempo utilizado foi gasto refatorando o que havia feito a primeira vez.
* Tenho noção que é possível fazer uma outra refatoração para deixar mais simples, visto que os controllers são bem semelhantes, talvez seja possível ter um controller base que faça a maior parte das ações padrões e deixar nas implementações desse controller base as particularidades de cada um.

Passos utilizados:
* Criação dos Models.
* Adicionados pacotes System.Net.Http e Microsoft.AspNet.WebApi.Client
* Adicionadas as views de Motorista.
* Adicionado o Controller de Motorista.
  * Codificação dos métodos base para utilização do controller.
  * Refatoração do código para deixar algumas funcionalidades mais genéricas.
* Adicionadas as views de Viagem.
* Adicionado o Controller de Viagem.
