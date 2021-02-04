import React from 'react'
import Msg from './Msg'

export default function MsgList({ msgs }) {
  return (
    msgs.map(msg => {
      return <Msg msg={msg} />
    })
  )
}
