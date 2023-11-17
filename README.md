# Documentação da API de Votação

Esta documentação detalha a estrutura, os endpoints e as regras de negócio da API de Votação. A API gerencia sessões de votação em uma cooperativa, proporcionando funcionalidades como cadastrar pautas, abrir sessões de votação, receber votos e contabilizar resultados.

## Sumário

- [1. Estrutura da API](#1-estrutura-da-api)
- [2. Endpoints](#2-endpoints)
  - [2.1 Health Check](#21-health-check)
  - [2.2 Pautas](#22-pautas)
    - [2.2.1 Listar Todas as Pautas](#221-listar-todas-as-pautas)
    - [2.2.2 Obter Detalhes de uma Pauta](#222-obter-detalhes-de-uma-pauta)
    - [2.2.3 Criar uma Nova Pauta](#223-criar-uma-nova-pauta)
    - [2.2.4 Atualizar uma Pauta](#224-atualizar-uma-pauta)
    - [2.2.5 Excluir uma Pauta](#225-excluir-uma-pauta)
  - [2.3 Votos](#23-votos)
    - [2.3.1 Registrar um Voto](#231-registrar-um-voto)
    - [2.3.2 Obter Resultado de uma Votação](#232-obter-resultado-de-uma-votação)
- [3. Regras de Negócio](#3-regras-de-negócio)
  - [3.1 Cadastro de Pautas](#31-cadastro-de-pautas)
  - [3.2 Sessões de Votação](#32-sessões-de-votação)
  - [3.3 Registro de Votos](#33-registro-de-votos)
  - [3.4 Contabilização de Resultados](#34-contabilização-de-resultados)
- [4. Tarefas Bônus](#4-tarefas-bônus)
  - [4.1 Integração com Sistemas Externos](#41-integração-com-sistemas-externos)
  - [4.2 Mensageria e Filas](#42-mensageria-e-filas)
  - [4.3 Performance](#43-performance)
  - [4.4 Versionamento da API](#44-versionamento-da-api)
- [5. Objetos DTO](#5-objetos-dto)
  - [5.1 ScheduleDTO](#51-scheduledto)
  - [5.2 VoteDTO](#52-votedto)

## 1. Estrutura da API

A API é organizada em três controladores principais: `HealthController`, `ScheduleController` e `VoteController`. Cada controlador lida com diferentes aspectos da aplicação, como status da API, gerenciamento de pautas e registro de votos.

## 2. Endpoints

### 2.1 Health Check

- **Endpoint:** `/api/health`
- **Método:** `GET`
- **Descrição:** Verifica o status da API.

### 2.2 Pautas

#### 2.2.1 Listar Todas as Pautas

- **Endpoint:** `/api/schedule`
- **Método:** `GET`
- **Descrição:** Retorna a lista de todas as pautas cadastradas.
- **Retorno:** Array de objetos `ScheduleDTO`.

#### 2.2.2 Obter Detalhes de uma Pauta

- **Endpoint:** `/api/schedule/{id}`
- **Método:** `GET`
- **Parâmetros:**
  - `id` (long): Identificador único da pauta.
- **Descrição:** Retorna os detalhes de uma pauta específica com base no ID fornecido.
- **Retorno:** Objeto `ScheduleDTO`.

#### 2.2.3 Criar uma Nova Pauta

- **Endpoint:** `/api/schedule`
- **Método:** `POST`
- **Corpo da Requisição:** Objeto `ScheduleDTO`
- **Descrição:** Cria uma nova pauta com base nos dados fornecidos.
- **Retorno:** Objeto `ScheduleDTO` da pauta criada.

#### 2.2.4 Atualizar uma Pauta

- **Endpoint:** `/api/schedule`
- **Método:** `PUT`
- **Corpo da Requisição:** Objeto `ScheduleDTO`
- **Descrição:** Atualiza os dados de uma pauta existente.
- **Retorno:** Objeto `ScheduleDTO` da pauta atualizada.

#### 2.2.5 Excluir uma Pauta

- **Endpoint:** `/api/schedule/{id}`
- **Método:** `DELETE`
- **Parâmetros:**
  - `id` (long): Identificador único da pauta.
- **Descrição:** Exclui uma pauta com base no ID fornecido.
- **Retorno:** Objeto `ScheduleDTO` da pauta excluída.

### 2.3 Votos

#### 2.3.1 Registrar um Voto

- **Endpoint:** `/api/vote`
- **Método:** `POST`
- **Corpo da Requisição:** Objeto `VoteDTO`
- **Descrição:** Registra um voto em uma pauta específica com base nos dados fornecidos.
- **Retorno:** Mensagem de sucesso.

#### 2.3.2 Obter Resultado de uma Votação

- **Endpoint:** `/api/vote`
- **Método:** `GET`
- **Descrição:** Obtém o resultado da votação para as pautas encerradas.
- **Retorno:** Não tem retorno, será enviado por mensagem no RabbitMQ um JSON com o resultado da votação

## 3. Regras de Negócio

### 3.1 Cadastro de Pautas

1. Ao criar uma nova pauta, se a data de encerramento não for fornecida, será automaticamente definida como 1 minuto após a data de início.
2. Não é permitido alterar uma pauta se já existirem votos computados para ela.
3. Não é permitido deletar uma pauta se já existirem votos computados para ela.

### 3.2 Registro de Votos

1. Um associado pode votar apenas uma vez em cada pauta.
2. Um associado é identificado pelo CPF.

### 3.3 Contabilização de Resultados

1. O resultado da votação só é disponibilizado após o encerramento da sessão de votação.
2. O resultado é composto pelo número de votos "S", "N" e o total de votos.

## 4. Tarefas Bônus

### 4.1 Integração com Sistemas Externos

1. A API integra-se a um sistema externo para verificar a validade do CPF do associado antes de registrar o voto.

### 4.2 Mensageria e Filas

1. O resultado da votação é enviado para outros sistemas por meio de mensageria.

### 4.3 Performance

1. A aplicação é projetada para lidar com cenários de alto desempenho, suportando centenas de milhares de votos.

### 4.4 Versionamento da API

1. Estratégias apropriadas de versionamento de API são adotadas para garantir a compatibilidade e evolução controlada.

## 5. Objetos DTO

### 5.1 ScheduleDTO

```
{
  "id": 1,
  "description": "Pauta 01",
  "startingDate": "2023-11-17T12:01:34.244Z",
  "finishingDate": "2023-11-17T12:01:34.244Z"
}
```

### 5.2 VoteDTO
```
{
  "cpf": "12345678901",
  "scheduleId": 1,
  "voteOption": "S"
}
```

Objeto utilizado para representar dados de votos na API. [Detalhes](#52-votedto).

Esses objetos são usados para transferir dados entre a API e os clientes, garantindo uma comunicação eficiente e padronizada.