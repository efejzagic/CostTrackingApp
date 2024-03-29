import React, { useState, useEffect } from "react";
import { Button, Container, Paper, TextField, Typography } from "@mui/material";
import axios from "axios";
import "react-toastify/dist/ReactToastify.css";
import { toast } from "react-toastify";
import { Link } from "react-router-dom";
import {
  /* ... */ FormControl,
  InputLabel,
  Select,
  MenuItem
} from "@mui/material";
import StyledPage from "../../components/Styled/StyledPage";
import { getConfigHeader } from "../../components/Auth/GetConfigHeader";
import { useSelector, useDispatch } from "react-redux";
import { selectSuppliers, loadsuppliers } from "../../state/suppliers";
import { selectArticles, loadArticles, addArticle } from "../../state/articles";

const CreateArticlePage = () => {
  const [formData, setFormData] = useState({
    Name: "",
    Quantity: "",
    Price: 0,
    Description: "",
    SupplierId: 0
  });

  const dispatch = useDispatch();
  const suppliers = useSelector(selectSuppliers);
  const articles = useSelector(selectArticles);

  useEffect(() => {
    dispatch(loadsuppliers());
    dispatch(loadArticles());
  }, []);

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value
    }));
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    try {
      dispatch(addArticle(formData));
    } catch (error) {
      console.error("Error:", error);
      toast.error("Fail");
    }
  };
  return (
    <>
      <StyledPage>
        <Container maxWidth="md" style={{ marginTop: "2rem" }}>
          <Typography variant="h5" gutterBottom>
            New Article
          </Typography>
          <Paper elevation={3} style={{ padding: "2rem" }}>
            <form onSubmit={handleSubmit}>
              <TextField
                label="Name"
                name="Name"
                fullWidth
                required
                value={formData.Name}
                onChange={handleInputChange}
                style={{ marginBottom: "1rem" }}
              />
              <TextField
                label="Quantity"
                name="Quantity"
                fullWidth
                required
                value={formData.Quantity}
                onChange={handleInputChange}
                style={{ marginBottom: "1rem" }}
              />
              <TextField
                label="Price"
                name="Price"
                fullWidth
                required
                value={formData.Price}
                onChange={handleInputChange}
                style={{ marginBottom: "1rem" }}
              />
              <TextField
                label="Description"
                name="Description"
                fullWidth
                required
                value={formData.Description}
                onChange={handleInputChange}
                style={{ marginBottom: "1rem" }}
              />

              <FormControl fullWidth style={{ marginBottom: "1rem" }}>
                <InputLabel id="supplier-label">Supplier</InputLabel>
                <Select
                  labelId="supplier-label"
                  id="SupplierId"
                  name="SupplierId"
                  value={formData.SupplierId}
                  onChange={handleInputChange}
                  required
                  MenuProps={{
                    style: { maxHeight: "400px" }
                  }}
                >
                  {suppliers.map((supplier) => (
                    <MenuItem key={supplier.id} value={supplier.id}>
                      {supplier.name}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>

              <Button type="submit" variant="contained" color="primary">
                Create
              </Button>
            </form>
          </Paper>
          <Button>
            {" "}
            <Link to={`/article`}>Back to Articles</Link>{" "}
          </Button>
        </Container>
      </StyledPage>
    </>
  );
};

export default CreateArticlePage;
