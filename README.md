# Projeto Conexa

### Arquitetura

<p>O padrão de arquitetura usada na API foi a <b>Arquitetura Limpa</b> (Clean Architecture) composto por três projetos principais, sendo eles o Application Core, a Infra.Data e a WebApplication (Usuário). Junto com eles temos a infra.IoC (inversion of Control) e o XUnitTest (Os testes unitários).</p>

![Imagem_do_projeto](https://i.ibb.co/55RwwwR/image.png)

Com essa metodologia de desenvolvimento, o núcleo do aplicativo (Aqui chamada como Application) não tem qualquer depêndencia da Infra e muito menos da WebApi, facilitando o uso de testes unitários dentro dos serviços do Core.

![Imagem da Arquiterua](https://docs.microsoft.com/pt-br/dotnet/architecture/modern-web-apps-azure/media/image5-8.png)
<p></p>

#### WebApplication

<p>Começando pela camada de usuário, nesse projeto foi inicializado apenas uma pasta com a controller responsável pela requisição da temperatura, o startup responsável pelas configurações iniciais do sistema e a configuração do docker</p>

![Imagem_WebApplication](https://i.ibb.co/Lhq0dDj/image.png)

#### Infrastructure.IoC

<p>Este projeto é responsável pelo aclopamento das interfaces com suas classes implementadas</p>

![Imagem_Infrastructure.IoC](https://i.ibb.co/3Tdr8dP/image.png)

#### Infra.Data

<p>Este projeto é responsável pela coleta e manuseio dos dados, tanto do banco de dados quanto da busca das informações pela API.</p>
<p>Também é responsável pela criação do banco utilizando o <b>Code First</b> com as migrations.</p>
<p>As informações da API são recolhidas na classe APIExternalWeatherMaps e as informações no banco são através da pasta Repositories</p>

![Imagem_Infra.Data](https://i.ibb.co/mXf15HB/image.png)

### Application

<p> Coração do projeto, é onde as regras de negócio, Entidades, Mapeamento e as ViewModel estão. Responsável pelo processamento das informações recebidas e as informações que serão enviadas.</p>

![Imagem_Application](https://i.ibb.co/3khb6bL/image.png)

## xUnitTest

<p>Projeto responsável pelos testes unitários da aplicação.</p>

### Rodando o projeto


#### Rodando o projeto na máquina local

<p>Para executar o projeto em sua máquina local, certifique-se de que esteja com o banco de dados MySql instalado e executando</p>
<p>Altere as string de conexão dos arquivos em <i>Infra.Data->DBContext->ConexaoDesignContextFacotry.cs</i> e <i>WebApplication->Startup-> e mude em RegisterServices()</i></p>
<p>*Execute a WebApplication*</p>


<p>O sitema foi encapsulado em docker</p>
<p>Para rodar os comandos Docker utilize os comandos `docker run -d --name desafio-csharp-easy -p 5000:80 brenodml/desafioteste:latest`</p>
<p>Obsj. Certifique de alterar as configurações do banco de dadocd s antes de executar o container.</p>
