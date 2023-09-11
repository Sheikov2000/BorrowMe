import React, { useContext, useEffect, useLayoutEffect, useState } from "react";
import { Button, Form, FormGroup, Input, Label } from "reactstrap";
import NavBar from "../components/NavBar";
import { userContext } from "../App";
import { addItem } from "../modules/itemMenager"; 
import { getAllCategories } from "../modules/categoryManager";
import { useNavigate } from "react-router-dom";

//UserId - auto populate on item with context
//Title - text field
//Description -text field
//ImageUrl - text field
//CategoryId - select

export const CreateItem = () => {
  const user = useContext(userContext);
  const [categories, setCategories] = useState([]);
  const [newItem, setNewItem] = useState({
    title: "",
    description: "",
    imageUrl: "",
    categoryId: 1,
    userId: 0,
  });
  const navigate = useNavigate();

  useLayoutEffect(() => {
    getAllCategories().then(setCategories);
  }, []);

  useEffect(() => {
    if (user != null) {
      const copy = { ...newItem };
      copy.userId = user.id;
      setNewItem(copy);
    }
  }, [user]);

  const submitItem = () => {
    addItem(newItem);
    navigate("/MyItems");
  };

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
            <Label for="category">Category</Label>
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