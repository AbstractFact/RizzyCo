import React from 'react'
import Player from './Player'

export default function PlayerList({ players, togglePlayer }) {
  return (
    players.map(player => {
      return <Player key={player.id} togglePlayer={togglePlayer} player={player} />
    })
  )
}
