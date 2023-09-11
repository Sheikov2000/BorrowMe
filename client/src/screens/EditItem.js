import React, { useEffect, useLayoutEffect, useState } from "react";
import { Button, Form, FormGroup, Input, Label } from "reactstrap";
import NavBar from "../components/NavBar";
import { editItem } from "../modules/itemMenager";
import { deleteItem } from "../modules/itemMenager";
import { getAllCategories } from "../modules/categoryManager";
import { useNavigate, useParams } from "react-router-dom";
import { getItemById } from "../modules/itemMenager";

export const EditItem = () => {
  //useParams because we navigate to EditItem/1
  //GetItemById with that route parameter

  //generate the form, BUT
  // prepoulate the data
  const [newItem, setNewItem] = useState({});
  const [categories, setCategories] = useState([]);
  const navigate = useNavigate();
  const { id } = useParams();
  const submitItem = () => {
    editItem(newItem);
      navigate("/MyItems")
  };


  useEffect(() => {
    delete newItem.user;
    delete newItem.category;
  }, [newItem]);



  useLayoutEffect(() => {
    getAllCategories().then(setCategories);
    getItemById(id).then(setNewItem)

  }, []);

  if (newItem) {

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
                value={newItem.title}
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
                value={newItem.description}
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
                value={newItem.imageUrl}
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
                value={newItem.categoryId}
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
            <Button onClick={deleteItem}>Delete</Button>
          </Form>
        </div>
      </>
    );
  }
};