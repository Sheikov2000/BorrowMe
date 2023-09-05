import React, { useState } from "react";
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavbarText,
} from "reactstrap";

const NavBar = ({ args }) => {
  const [isOpen, setIsOpen] = useState(false);

  const toggle = () => setIsOpen(!isOpen);

  return (
    <div>
      <Navbar {...args} style={{ backgroundColor: "#D9D9D9" }}>
        <NavbarBrand href="/">
          <p
            style={{
              fontFamily: "Cherry Bomb One",
              color: "#7CC6FE",
              fontSize: "48px",
            }}
          >
            Borrow Me!
          </p>
        </NavbarBrand>
        <NavbarText>All Items</NavbarText>
        <NavbarText>My Items</NavbarText>
        <NavbarText>My Messages</NavbarText>
        <NavbarToggler onClick={toggle}></NavbarToggler>
        <Collapse isOpen={isOpen} navbar>
          <Nav navbar>
            <NavItem>Logout</NavItem>
          </Nav>
        </Collapse>
      </Navbar>
    </div>
  );
};

export default NavBar;