import React, { useEffect } from "react";
import { getAllUserItems } from "../modules/itemMenager";
import { useState, useLayoutEffect, useContext } from "react";
import NavBar from "../components/NavBar";
import ItemCard from "../components/itemCard/ItemCard";
import { userContext } from "../App";
import { Button } from "reactstrap";
import { Link, useNavigate } from "react-router-dom";

export const MyItems = () => {
  const [myItems, setMyItems] = useState([]);
  const user = useContext(userContext);
  const navigate = useNavigate();

  const navigateToCreate = () => {
    navigate("Add");
  };

  useEffect(() => {
    if (user != null) {
      getAllUserItems(user.id).then(setMyItems);
    }
  }, [user]);

  return (
    <>
      <NavBar />
      <div className="allItems">
        <h1 style={{ fontFamily: "Cherry Bomb One", fontSize: "72px" }}>
          My Items
        </h1>
        <div className="itemCards">
          {myItems.map((item) => (
            <Link key={item.id} to={`Edit/${item.id}`}>
              <ItemCard
                title={item.title}
                body={item.description}
                imageSource={item.imageUrl}
              />
            </Link>
          ))}
          <Button
            size="lg"
            style={{ position: "absolute", right: 20, bottom: 20 }}
            onClick={navigateToCreate}
          >
            Add Item
          </Button>
        </div>
      </div>
    </>
  );
};