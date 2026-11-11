# Drunk-Mage

**Drunk-Mage** is a single-player, push-your-luck deckbuilding card game developed in Unity.

Play as a wizard who uses magical potions to attack enemies, defend against incoming damage, and survive challenging encounters. Every potion increases your drunkenness, forcing you to balance powerful combinations against the risk of losing control.

## Gameplay

The main mechanic revolves around the **Drunkenness System**.

Each card increases the player's drunkenness by a specific amount. Players can continue playing cards and drawing additional potions, but reaching the drunkenness limit immediately ends their turn and cancels their pending effects.

The goal is to build effective card combinations while managing risk.

## Features

- **Push-your-luck gameplay** — decide whether to keep playing or stop before reaching the drunkenness limit.
- **Card-based combat** — use attack, defense, healing, and utility potions.
- **Risky Draw** — draw and automatically play an additional card.
- **Delayed Effects** — accumulate effects that resolve at the end of the turn.
- **Turn-based combat** — survive enemy attacks while managing health and shields.

## Controls

- **Mouse** — interact with cards and buttons.
- **Drag and drop** — play cards and select targets when required.

## Built With

- Unity 6
- C#
- DOTween
- TextMeshPro

## How to Play

1. Download the Windows build.
2. Extract the ZIP archive.
3. Run `Drunk-Mage.exe`.

## Project Status

**Playable prototype — work in progress.**

This project was created to experiment with push-your-luck mechanics, card interactions, and gameplay systems in Unity.

## Development

The game uses an action-based architecture to manage card effects, combat, and turn progression. Gameplay mechanics are separated into dedicated systems to make the project easier to extend and maintain.
