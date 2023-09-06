import React, { useEffect, useState, useContext, useLayoutEffect } from "react";
import { Button, Form, FormGroup, Input, Label } from "reactstrap";
import { userContext } from "../App";
import NavBar from "../components/NavBar";
import { addItem } from "../modules/itemMenager";
import { getAllCategories } from "../modules/categoryManager";

//UserId - auto populate on item with context
//Title - text field
//Description -text field
//ImageUrl - text field
//CategoryId - select


export const CreateItem = () => {
  const user = useContext(userContext)
  const [categories, SetCategories] = useState([])
  const [newItem, setNewItem] = useState({
    title: "",
    description: "",
    imageUrl: "",
    categoryId: "",
    userId: 0,
  });

  useLayoutEffect(() => {
    getAllCategories().then(SetCategories)
  }

  )

const submitItem = () => {
  const copy = { ...newItem };
   copy.userId = user.id;
   setNewItem(copy);
   addItem(newItem);
}


  return (
    <>
      <NavBar />
      <div style={{ display: "flex", justifyContent: "center" }}>
        <Form style={{ width: "50%" }}>
          <FormGroup>
            <Label for="title">Title</Label>
            <Input
              id="title"
              name="title"
              placeholder="Enter Item Title"
              type="text"
              onChange={(event) => {
                const copy = { ...newItem };
                copy.title = event.target.value;
                setNewItem(copy);
              }}
            />
          </FormGroup>
          <FormGroup>
            <Label for="description">Description</Label>
            <Input
              id="description"
              name="description"
              placeholder="Enter Item Description"
              type="text"
              onChange={(event) => {
                const copy = { ...newItem };
                copy.description = event.target.value;
                setNewItem(copy);
              }}
            />
          </FormGroup>
          <FormGroup>
            <Label for="imageURL">Image URL</Label>
            <Input
              id="imageURL"
              name="imageURL"
              placeholder="Enter Item Image URL"
              type="text"
              onChange={(event) => {
                const copy = { ...newItem };
                copy.imageUrl = event.target.value;
                setNewItem(copy);
              }}
            />
          </FormGroup>
          <FormGroup>
            <Label for="categoryL">Image URL</Label>
            <Input
              id="category"
              name="category"
              type="select"
              onChange={(event) => {
                const copy = { ...newItem };
                copy.categoryId = parseInt(event.target.value);
                setNewItem(copy);
              }}
            >
              {categories.map((category) => (
                <option key={category.id} value={category.id}>
                  {category.name}
                </option>
              ))}
            </Input>
          </FormGroup>
          <Button onClick={submitItem}>Submit</Button>
        </Form>
      </div>
    </>
  );
};