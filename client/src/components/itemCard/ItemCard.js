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
        src={imageSource}
        width="90%"
        style={{ marginTop: "5%" }}
      />
      <CardBody>
        <CardTitle style={{ fontFamily: "Cherry Bomb One" }}>{title}</CardTitle>
        <CardText>{body}</CardText>
      </CardBody>
    </Card>
  );
};

export default ItemCard;