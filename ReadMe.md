# Morpion en Réseau - C#

Un jeu de morpion (tic-tac-toe) multijoueur en ligne, développé en C# avec une architecture client-serveur. Le projet fonctionne sous Visual Studio Code.

## Fonctionnalités

- Serveur dédié pour gérer les parties  
- Deux clients pouvant se connecter simultanément  
- Interface en console simple et intuitive  
- Détection automatique des victoires, défaites et matchs nuls  
- Communication réseau via TCP/IP

## Prérequis

- [.NET SDK 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)  
- [Visual Studio Code](https://code.visualstudio.com/)  
- Extension C# pour VS Code (C# for Visual Studio Code)

## Structure du projet

```bash
MorpionNetwork/
├── Server/   # Code du serveur
├── Client/   # Code du client
└── Shared/   # Code partagé entre client et serveur
```

## Installation
### Cloner le dépôt :

```bash
git clone [https://github.com/NathanNOVARESE/MorpionNetwork]
```

## Exécution
### Lancer le serveur (dans un terminal) :

```bash
cd Server
dotnet run
```
### Lancer le premier client (dans un second terminal) :

```bash
cd Client
dotnet run
```
Entrez localhost comme adresse IP.


### Lancer le second client (dans un troisième terminal) :

```bash
cd Client
dotnet run
```
Entrez localhost comme adresse IP.

## Comment jouer

Le premier client à se connecter joue avec les X.

Le second client joue avec les O.

À votre tour, entrez les coordonnées sous la forme : ligne colonne (ex. 1 2).

Le résultat de la partie s’affiche automatiquement à la fin.

## Commandes

Pour quitter :

```bash
Ctrl+C
```

## Personnalisation

Vous pouvez modifier certains paramètres dans le fichier Shared/GameProtocol.cs :

ServerPort : pour changer le port utilisé par le serveur

Messages : pour ajouter de nouveaux types de messages et étendre les fonctionnalités

## Auteur

Nathan NOVARESE & Adrien BOREE

