# cloud-sales-system

A .net web API using PostgreSQL as the database.
DB schema provided in the `db-schema.sql` file.

Environment variables:
- `ConnectionStrings.PostgresqlConnection`

Future improvements:
- Finishing up some endpoints
- Caching
- Auth
- Adding support for transactions using unit of work pattern
- Adding change tracking fields to entities
- Introducing prices
- Adding more complex searching and sorting functionalities
- Improving validation (new validation rules, extracting into validation modules)
- Indexing in the DB
- Adding SQL migrations
- etc.