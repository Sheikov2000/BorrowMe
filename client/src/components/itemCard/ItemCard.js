import React from "react";
import { Card, CardBody, CardText, CardTitle } from "reactstrap";

const ItemCard = ({ title, body, imageSource, onClick }) => {
  return (
    <Card
      style={{
        width: "18rem",
        alignItems: "center",
        borderRadius: "5%",
        overflow: "hidden",
      }}
    >
      <img
        alt="Card cap"
        src="https://picsum.photos/318/180"
        width="90%"
        style={{ marginTop: "5%" }}
      />
      <CardBody>
        <CardTitle style={{ fontFamily: "Cherry Bomb One" }}>Test</CardTitle>
        <CardText>
          Some quick example text to build on the card title and make up the
          bulk of the card's content.
        </CardText>
      </CardBody>
    </Card>
  );
};

export default ItemCard;