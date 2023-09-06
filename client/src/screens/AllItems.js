import React, { useLayoutEffect } from 'react'
import NavBar from "../components/NavBar"
import { getAllItems } from "../modules/itemMenager"
import ItemCard from "../components/itemCard/ItemCard";
import { useState } from "react";


export const AllItems = () => {
  
  const [allItems, setAllItems] = useState([]);

  useLayoutEffect(() => {
    getAllItems().then(setAllItems);
  }, [])


  return (
    <>
    <NavBar />
      <div className="allItems">
        <h1 style={{ fontFamily: "Cherry Bomb One", fontSize: "72px"}}>
        All Items
    </h1>
<div className="itemCards">
    {allItems.map((item) => (
      <ItemCard
      key={item.id}
      title={item.title}
      body={item.description}
      imageSource={item.imageUrl}
      />
    ))}
    </div>
</div>
    
    </>
  )
}
