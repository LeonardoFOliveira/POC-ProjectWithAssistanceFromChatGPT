# POC-ProjectWithAssistanceFromChatGPT
POC realizada para verificar a potencialidade que o Chat GPT pode proporcionar em redução de tempo no desenvolvimento.

## Premissas
Estudo realizado para aprender um pouco sobre Chat GPT e também sua pontecialidade no contexto de desenvolvimento de software.

No arquivo ChatGPT.pdf dentro deste repositório, está toda a conversa feita com o ChatGPT para a construção desse projeto.
Foi utilizado diretivas de negócio escrita por colegas de trabalho para iniciar o chat e os estudos.

A parte técnica foi levantada do zero junto ao chat GPT, porém foi necessário passar diretivas de padrões a serem seguidos para ser mais acertivo e seguir com um desenvolvimento de qualidade.

## Partes do Projeto realizada
- A parte de estórias, foi aproveitado o insumo de negócio dos colegas de trabalho para este fim.
- Implementações:
-- BackEnd (como microserviços / C# / .NET)
-- FrontEnd (Angular)
-- e2e (Teste automatizado - Cypress)

## Subida de ambiente
- Para subir o backend, encontrado na pasta BackEnd, é necessário colocar o projeto Web "EmployeePayrollAccess.API" como o de inicialização no Visual Studio;
- Para subir o frontend, encontrado na pasta FrontEnd, é necessário estar neste caminho e rodar o "ng serve" no terminal;
  -- Lembrando que precisar ter instalado o node para que consiga dar o "npm i" para instalar as dependências antes;
- Para subir o teste e2e, eonctrado na pasta cypress-e2e, é necessário estar neste caminho e rodar o "npx cypress open" (depois seguir os passos pela UI do Cypress);
  -- Lembrando que é necessário ter o cypress instalado na máquina;
  
## Conclusão
Conclui-se que o ChatGPT pode ser uma otima ferramenta para otimizar o tempo de desenvolvimento de software, principalmente quando quer rodar alguma POC ou MVP de algum produto, de forma que tenha algo o quanto antes. 
No entanto, fica claro, pelo menos por ora, a necessidade de ter o profissional da área que tenha o conhecimento amplo do fluxo de desenvolvimento pois pela conversa gerada fica claro que é necessário que direcione e corriga o próprio ChatGPT em suas respostas. Não é possível, pelo menos visto por este teste simplório, que tenha-se prompts fixos e ele te de respostas iguais todas vezes... pode-se ter resultados bem diferentes.
Pode dar a possibilidade de como times menores, terem desenvolvimentos mais otimizados. Mas pode haver contra-pontos...
Válido lembrar também das preocupações que se deve ter ao levar para um contexto de produção de software, em que o modelo de negócio precisaria ser todo revisto.

É uma faca de dois gumes para o mercado. 
Válido refletir sobre qual o objetivo e modelo de implementação de seu uso dentro do seu contexto.

  
