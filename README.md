Sistema de cadastro de fotos de viagem
OBS: envie junto com o projeto o script para criação das tabelas do banco de dados
1 – Página inicial do sistema (home) – Deve ser acessível mesmo sem ter feito login
2 - Tela de Login com opção de se cadastrar no sistema.
3 - Cadastro de usuário deve contemplar os campos: 
•	Nome (string)
•	Login  (string) (usar e-mail)
•	Senha (string)
4 -  Crie uma tela de cadastro de fotos de viagem.  Valide os dados da forma que achar melhor. (acessível apenas após login)
Campos para preenchimento:
•	Código  (obrigatório >=1)  (pode ser autonumeração)
•	O local onde a foto foi tirada  (string) (obrigatório)
•	A data onde a foto foi tirada  (datetime) (obrigatório uma data válida)
•	Até três fotos de viagem (ao menos uma é obrigatória)
•	Usuário: preencher com o código do usuário que fez login no sistema. Não é um campo para preenchimento, ele deve ser preenchido de forma automática pelo sistema. Atenção: durante a edição de um registro, este campo não deverá ser perdido.
•	Data da criação do registro – Data que o registro foi criado no sistema. Deve ser preenchido apenas na inclusão e de forma automática. Não é um campo editável, mas deve ser apresentado ao usuário no momento da edição de um registro.
•	Data da última alteração do registro. Deve ser preenchido apenas em alterações (edit). Não é um campo editável, mas deve ser apresentado ao usuário no momento da edição de um registro.
5 –  Apresentar fotos de viagem: (acessível apenas após login)
- Monte uma tela onde serão apresentadas todas as fotos de viagens de todos os usuários cadastrados no sistema, no seguinte formato:
Usuário 1: Ana Paula Silva – AnaPaula@Gmail.com.br
                                 
Local: São Bernardo do Campo			Local: São Caetano do Sul – Goiás		Local: Santo André	
Data:  01/02/2008                    			Data:  01/02/2012				Data:  01/09/2013


Usuário 2: YYYYYYYYYYYYYYYYYYY
