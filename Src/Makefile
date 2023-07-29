DOCKER = docker compose
DOTNET = dotnet ef

build: 
	$(DOCKER) build
.PHONY: build

start: 
	$(DOCKER) up --build
.PHONY: start

stop: 
	$(DOCKER) stop
.PHONY: stop

watch: 
	dotnet watch
.PHONY: watch

lint:
	dotnet format --verify-no-changes
.PHONY: lint

lint-fix:
	dotnet format
.PHONY: lint-fix

test:
	cd ../Test & dotnet test & cd ../Src
.PHONY: test

install:
	dotnet add package $(package)
.PHONY: install

deinstall:
	dotnet add remove $(package)
.PHONY: deinstall

migration: 
	$(DOTNET) migrations add $(name)
.PHONY: migrate

db-update: 
	$(DOTNET) database update
.PHONY: db-update

db-revert: 
	$(DOTNET) migrations remove
.PHONY: db-revert
