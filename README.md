# CrapetteSolver

## Présentation

Ce projet est une application dédiée à ma version du jeu de cartes de la Crapette. L'objectif principal est de développer un moteur d'intelligence artificielle capable de résoudre le jeu et de déterminer les stratégies optimales. En parallèle, ce travail sert à explorer les bonnes pratiques en architecture logicielle, notamment la modularité et la conteneurisation avec Docker.

## Fonctionnalités

* **Jeu de la Crapette :** Implémentation fidèle d'une version spécifique des règles.
* **Moteur d'IA :** Algorithmes de résolution (Monte Carlo, IA décisionnelle) pour l'analyse et la suggestion de coups.
* **API Backend :** ASP.NET Core en C# pour la logique du jeu et l'interaction avec l'IA.
* **Interface Utilisateur :** Application web interactive en React pour visualiser le jeu.
* **Architecture Modulaire :** Structuration claire des composants C# pour faciliter la maintenance et l'évolution.
* **Conteneurisation :** Utilisation de Docker pour un environnement de développement et de déploiement cohérent.

## Structure du Projet

Le projet est organisé en plusieurs répertoires pour une modularité optimale :
<pre>
├── CardGame.sln             # Fichier de solution Visual Studio
├── src/                     # Code source C#
│   ├── CardGame.Core/       # Fondations du jeu de cartes (agnostique)
│   ├── CardGame.GameRules/  # Implémentation des règles spécifiques de la Crapette
│   ├── CardGame.AI/         # Implémentation des algorithmes d'IA
│   └── CardGame.API/        # Point d'entrée de l'API web ASP.NET Core
├── tests/                   # Projets de tests (unitaires, intégration)
│   └── CardGame.Tests/
└── frontend/                # Application web React
</pre>



### Dépendances entre les modules C# :

* `CardGame.API` dépend de `CardGame.GameRules` et `CardGame.AI`.
* `CardGame.AI` dépend de `CardGame.GameRules`.
* `CardGame.GameRules` dépend de `CardGame.Core`.
* `CardGame.Core` est indépendant des autres projets.

## Règles de la Crapette

Les règles s'apparentent globalement à celles trouvées sur wikipedia : https://fr.wikipedia.org/wiki/Crapette. Les spécificités de ma version sont que la carte supérieure de la crapette n'est pas retournée, et on peut mettre toute carte de la même couleur et de valeur adjacente ou égale sur la défausse adverse (8C -> 7C, 8C ou 9C). On ne peut pas interrompre l'adversaire en disant "Crapette !".


## Technologies

* **Backend :** C# (.NET), ASP.NET Core
* **Frontend :** React
* **Conteneurisation :** Docker, Docker Compose
* **Tests :** xUnit

## Démarrage

1.  **Cloner le dépôt :**
    ```bash
    git clone [https://github.com/Watashiku/CrapetteSolver.git](https://github.com/Watashiku/CrapetteSolver.git)
    cd CrapetteSolver
    ```
2.  **Lancer l'application avec Docker Compose :**
    ```bash
    docker-compose up --build
    ```
3.  **Accéder à l'application :** Le frontend sera accessible via votre navigateur (généralement `http://localhost:3000`).

## Contribution

Ce repo est avant tout un projet perso pour mettre en pratique les éléments appris lors de veille technique. Néanmoins toutes les contributions sont les bienvenues. N'hésitez pas à ouvrir des issues pour les bugs ou suggestions, ou à soumettre des pull requests.

## Licence

Ce projet est sous licence MIT.
