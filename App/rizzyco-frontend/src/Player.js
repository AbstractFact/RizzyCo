import React from 'react'

export default function Player({ player, togglePlayer }) {
  function handlePlayerClick() {
    togglePlayer(player.id)
  }
  
  return (
    <div>
      <label>
        <input type="checkbox" checked={player.complete} onChange={handlePlayerClick} />
        {player.username}
      </label>
    </div>
  )
}
