import React, { useState } from "react";
import { Button, Container, Paper, TextField, Typography } from "@mui/material";
import axios from "axios";
import "react-toastify/dist/ReactToastify.css";
import { toast } from "react-toastify";
import { useParams } from "react-router-dom";
import { useEffect } from "react";
import { Link } from "react-router-dom";
import {
  /* ... */ FormControl,
  InputLabel,
  Select,
  MenuItem
} from "@mui/material";
import StyledPage from "../../components/Styled/StyledPage";
import { getConfigHeader } from "../../components/Auth/GetConfigHeader";
import { useDispatch, useSelector } from "react-redux";
import { loadsuppliers, selectSuppliers } from "../../state/suppliers";
import { editArticle, selectArticle } from "../../state/articles";

const EditArticlePage = () => {
  const { id } = useParams();

  const [formData, setFormData] = useState({
    Id: id,
    Name: "",
    Quantity: 0,
    Price: 0.0,
    Description: "",
    SupplierId: ""
  });

  const dispatch = useDispatch();
  const article = useSelector(selectArticle(id));
  const suppliers = useSelector(selectSuppliers);

  useEffect(() => {
    const fetchArticleData = async () => {
      try {
        const mapperArticleData = {
          Name: article.name,
          Quantity: article.quantity,
          Price: article.price,
          Description: article.description,
          SupplierId: article.supplierId
        };
        setFormData((prevFormData) => ({
          ...prevFormData,
          ...mapperArticleData
        }));
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchArticleData();
  }, [id, article]);

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
      dispatch(editArticle(formData));
    } catch (error) {
      console.error("Error:", error);
      toast.error("Fail");
    }
  };

  useEffect(() => {
    dispatch(loadsuppliers());
  }, []);

  return (
    <>
      <StyledPage>
        <Container maxWidth="md" style={{ marginTop: "2rem" }}>
          <Typography variant="h5" gutterBottom>
            Edit Article
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
                type="number"
                fullWidth
                required
                value={formData.Quantity}
                onChange={handleInputChange}
                style={{ marginBottom: "1rem" }}
              />
              <TextField
                label="Price"
                name="Price"
                type="number"
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
                  value={formData.SupplierId ?? ""}
                  onChange={handleInputChange}
                  required
                  MenuProps={{
                    style: { maxHeight: "400px" }
                  }}
                >
                  {suppliers.map((site) => (
                    <MenuItem key={site.id} value={site.id}>
                      {site.name}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>

              <Button type="submit" variant="contained" color="primary">
                Edit
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

export default EditArticlePage;
