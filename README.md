# Fluxo De Caixa
Sistema de fluxo de caixas e lançamentos.

# Requisitos

## Net Core
- Net Core 7 (Caso não tenha baixe aqui https://dotnet.microsoft.com/download/dotnet/7.0)

## Instalação dos pacotes
No repositório Git será encontrado a pasta Nuget Packages. Lá contém os pacotes publicados:
•	Dietcode Api Core – Gerenciador de retorno da API
•	Dietcode Api Core Results – Tipos de retorno e resultados da api e serviço
•	Dietcode Domain Validator – É o pacote do Validation Result
•	Dietcode Lib – Lib de Helper.

### Use o comando

dotnet nuget push <caminho do arquivo >\nome.nupkg -s <caminho destino>
  
É provável que precise usar o command prompt como administrador e ir até a pasta que tem o nuget.exe que está dentro das pastas do visual Studio.
  
  ### Add o site de origem
  http://nuget.dietcode.com.br/nuget
  Nas opções do Visual Studio vc pode por os pacotes via webn sem precisar de rodar. Esta é uma opção mais segura e facil
  
  ##Setup Banco.
 No repositório existe uma pasta SQL. Dentro dela tem uma cópia de backup do banco. E em outra pasta os scripts de criação do banco, duas tabelas e duas procedures.
O projeto se rodar vai apontar para este banco: Server=dietcode.com.br\\Dev. Ele está em minha casa em um servidor meu interno. É um servidor genérico de dev. Não tem problemas o uso e abuso de inserir e mexer nos dados.

  ## Instalação
  
Existem N opções.
### Método um
Antes de tudo criar uma pasta e criar um site no iis, com DNS, ou na pasta criada adicionara aplicação/diretório virtual para que possa colocar o app publicado.
A mais simples é no repositório GIT clonar o projeto e na pasta publish tem a última versão publicada já, é só implementar no iis. E chamar. POIS está configurado para um banco online e vai rodar.
### Método dois
Criar o banco no servidor interno ai de vocês. Pode ser via restore do banco (.bak) que está disponibilizado junto do repositório Git. OU rodar os scripts na ordem:
1.	Criar banco
2.	Rodar scripts para criar tabela de tipo de lançamento
3.	Rodar scripts para criar tabela de Lançamento
4.	Rodar scripts para criar as procedures
Após isso alterar a string de conexão para conectar-se ao banco. É preciso dar permissões ao usuário que vai ser usado para conectar a essa base. Poderia usar o SA, porém, como é um ambiente corporativo é descartada esta possibilidade.

  
 ### Thanx! 
Teste Apply for Job. Made with good vibrations!
