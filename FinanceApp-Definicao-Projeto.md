# FinanceApp — Definição do Projeto

> Documento gerado em 2026-05-30 via entrevista estruturada.

---

## 1. Visão Geral

Aplicativo web de **gerenciamento financeiro pessoal** com foco em **educação integrada**: além de registrar receitas e despesas, o app analisa os dados do usuário e gera insights e recomendações automáticas para ajudá-lo a tomar melhores decisões financeiras.

O aplicativo deve funcionar em qualquer dispositivo e tamanho de tela (desktop, tablet e celular), com uma experiência responsiva e de qualidade.

---

## 2. Público-Alvo

- **Perfil principal:** Pessoa física em geral — desde quem está começando a organizar as finanças até quem já tem hábito financeiro estabelecido.
- **Uso inicial:** Pessoal (single-user), sem intenção comercial imediata.
- **Contexto do criador:** Conhecimento avançado em educação financeira e investimentos, o que orienta a profundidade dos insights gerados pelo app.

---

## 3. Proposta de Valor e Diferencial

O diferencial central **não é o registro de dados**, mas o que o app faz com eles:

> "O app entende seus hábitos financeiros e te ensina a melhorá-los — no momento certo, com base nos seus próprios dados."

### Como isso se manifesta:
- **Insights automáticos:** comparações entre períodos, desvios de orçamento, padrões de gasto.  
  Exemplo: *"Você gastou 38% a mais com alimentação este mês em relação à média dos últimos 3 meses."*
- **Alertas e recomendações:** sugestões ativas baseadas em regras financeiras sólidas.  
  Exemplo: *"Você ainda não atingiu 3 meses de reserva de emergência. Com base nos seus gastos médios, isso equivale a R$X."*

---

## 4. Módulos do MVP

### 4.1 Lançamento de Receitas e Despesas
- Registro manual de transações
- Campos: data, valor, categoria, descrição, tipo (receita/despesa)
- Categorias personalizáveis pelo usuário
- Suporte a transações recorrentes (fixas mensais)

### 4.2 Orçamento Mensal
- Definição de limite de gasto por categoria
- Acompanhamento em tempo real do realizado vs. planejado
- Indicadores visuais de progresso (ex.: barra de progresso por categoria)
- Alerta quando o orçamento de uma categoria está prestes a esgotar

---

## 5. Insights e Alertas (Regras de Educação Financeira)

Os insights são gerados por **lógica determinística no backend** — sem IA generativa, priorizando previsibilidade e performance.

### Exemplos de regras planejadas:
| Regra | Trigger | Mensagem |
|---|---|---|
| Variação de gasto | Categoria com >20% de aumento mês a mês | "Seu gasto com X cresceu Y% em relação ao mês anterior." |
| Orçamento crítico | Categoria com >80% do limite atingido antes do dia 20 | "Você usou 80% do orçamento de X antes do meio do mês." |
| Saldo positivo expressivo | Sobra >30% da renda no mês | "Você teve uma boa sobra este mês. Considere aportar esse valor em sua reserva de emergência." |
| Regra 50-30-20 | Análise mensal dos gastos totais | "Seus gastos com necessidades representam X% da sua renda — o ideal é até 50%." |
| Ausência de reserva | Sem categoria de "reserva de emergência" | "Você ainda não registrou uma reserva de emergência. Esse é o primeiro passo da saúde financeira." |

> As regras devem ser extensíveis — o backend deve permitir adicionar novas regras sem alterar a estrutura central.

---

## 6. Design e UX

### Filosofia de interface
- **Dashboard-first:** a tela inicial concentra a visão geral completa — saldo do mês, progresso de orçamentos por categoria, alertas ativos e resumo de receitas vs. despesas.
- **Responsivo por design:** todas as telas devem funcionar igualmente bem em mobile e desktop, sem versões separadas.
- **Clareza antes de estética:** dados legíveis, hierarquia visual clara, sem poluição visual.

### Componentes esperados na Dashboard
- Saldo do mês (receitas - despesas)
- Barra de progresso do orçamento total
- Cards de categorias com status (ok / atenção / crítico)
- Lista dos últimos lançamentos
- Painel de insights e alertas ativos

---

## 7. Stack Tecnológica

| Camada | Tecnologia |
|---|---|
| Frontend (SPA) | Angular 21 |
| Backend (API REST) | .NET (C#) |
| Banco de dados | SQLite (desenvolvimento local) |
| Autenticação | Single-user — sem login por enquanto |
| ORM | Entity Framework Core |

### Decisões arquiteturais orientadoras
- API RESTful seguindo convenções padrão do .NET
- Frontend Angular com standalone components (padrão Angular 17+)
- EF Core com migrations para controle do schema
- SQLite pode ser substituído por PostgreSQL ou SQL Server no futuro sem impacto no domínio
- Separação clara entre domínio (regras de negócio), aplicação e infraestrutura (Clean Architecture ou similar)

---

## 8. O que está FORA do escopo inicial

Para manter o foco, os itens abaixo **não fazem parte do MVP**:

- Multiusuário / autenticação
- Importação de extratos (OFX, CSV)
- Integração com Open Finance / Open Banking
- Módulo de investimentos / carteira de ativos
- Metas financeiras de longo prazo
- App nativo (iOS/Android)
- Geração de insights via IA/LLM
- Relatórios exportáveis (PDF, Excel)

Estes itens podem ser incorporados em versões futuras.

---

## 9. Princípios do Projeto

1. **Dados primeiro:** o app só é útil se o usuário lançar dados consistentemente — a UX deve facilitar ao máximo esse hábito.
2. **Educação contextual:** insights aparecem no contexto certo, não como notificações genéricas.
3. **Simples de usar, poderoso por baixo:** interface simples para o usuário final, arquitetura sólida e extensível por baixo.
4. **Mobile como cidadão de primeira classe:** nenhuma funcionalidade deve ser acessível apenas no desktop.
5. **Regras financeiras fundamentadas:** os insights devem refletir conhecimento sólido de educação financeira (regra 50-30-20, reserva de emergência, controle de gastos variáveis, etc.).

---

## 10. Próximos Passos Sugeridos

- [ ] Definir estrutura de pastas e arquitetura do projeto (backend e frontend)
- [ ] Modelar o banco de dados (entidades: Transação, Categoria, Orçamento, Insight)
- [ ] Criar o projeto .NET com EF Core + SQLite
- [ ] Criar o projeto Angular com estrutura de módulos/componentes
- [ ] Implementar CRUD de transações (backend + frontend)
- [ ] Implementar lógica de orçamento mensal
- [ ] Implementar motor de regras de insights
- [ ] Construir a Dashboard com os componentes definidos
