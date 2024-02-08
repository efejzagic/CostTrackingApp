import React, { useState, useEffect } from "react";
import axios from "axios";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Modal,
  Button,
  Box,
  Typography,
  IconButton,
  Container
} from "@mui/material";
import AddShoppingCartIcon from "@mui/icons-material/AddShoppingCart";
import StyledPage from "../../components/Styled/StyledPage";
import { useCart } from "../Cart/CartContext";
import LoadingCoomponent from "../../components/Loading/LoadingComponent";
import { useNavigate } from "react-router-dom";
import { getConfigHeader } from "../../components/Auth/GetConfigHeader";
import { useSelector, useDispatch } from "react-redux";
import { selectOrdersLoading } from "../../state/orders";
import { selectArticles, loadArticles } from "../../state/articles";

const OrderPage = () => {
  const articles = useSelector(selectArticles);
  const [selectedArticle, setSelectedArticle] = useState(null);
  const [quantity, setQuantity] = useState(1);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const { addToCart } = useCart();
  const isLoading = useSelector(selectOrdersLoading);
  const dispatch = useDispatch();
  const navigate = useNavigate();
  useEffect(() => {
    dispatch(loadArticles());
  }, []);

  const orderItemsData = JSON.parse(localStorage.getItem("cart"));

  const openModal = (article) => {
    setSelectedArticle(article);
    setIsModalOpen(true);
  };

  const closeModal = () => {
    setSelectedArticle(null);
    setIsModalOpen(false);
  };

  const handleAddToCart = () => {
    //add to cart reducer
    addToCart({
      id: selectedArticle.id,
      name: selectedArticle.name,
      quantity: quantity,
      price: selectedArticle.price
    });
    console.log("Item added to cart");
    closeModal();
    setQuantity(1);
  };

  return (
    <>
      <StyledPage>
        <div>
          <Container
            maxWidth="md"
            style={{
              marginTop: "2rem",
              display: "flex",
              flexDirection: "column",
              alignItems: "center"
            }}
          >
            <Button
              variant="contained"
              onClick={() => navigate("/order/history")}
              color="primary"
              style={{ marginBottom: "1rem", marginLeft: "1rem" }}
            >
              Order History
            </Button>
            {isLoading ? (
              <LoadingCoomponent loading={isLoading} />
            ) : (
              <TableContainer component={Paper}>
                <Table>
                  <TableHead>
                    <TableRow>
                      <TableCell width={100}>Id</TableCell>
                      <TableCell width={200}>Name</TableCell>
                      <TableCell width={200}>Quantity</TableCell>
                      <TableCell width={200}>Price</TableCell>
                      <TableCell width={200}>Description</TableCell>
                      <TableCell width={200}>Supplier</TableCell>
                      <TableCell width={100}></TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    {articles.map((article) => (
                      <TableRow key={article.id}>
                        <TableCell>{article.id}</TableCell>
                        <TableCell>{article.name}</TableCell>
                        <TableCell>{article.quantity}</TableCell>
                        <TableCell>{article.price}</TableCell>
                        <TableCell>{article.description}</TableCell>
                        <TableCell>{article.supplier.name}</TableCell>
                        <TableCell>
                          <IconButton onClick={() => openModal(article)}>
                            <AddShoppingCartIcon />
                          </IconButton>
                        </TableCell>
                      </TableRow>
                    ))}
                  </TableBody>
                </Table>
              </TableContainer>
            )}
          </Container>
          <Modal
            open={isModalOpen}
            onClose={closeModal}
            aria-labelledby="modal-title"
            aria-describedby="modal-description"
          >
            <Box
              sx={{
                position: "absolute",
                top: "50%",
                left: "50%",
                transform: "translate(-50%, -50%)",
                width: 300,
                backgroundColor: "white",
                border: "2px solid #000",
                boxShadow: 24,
                p: 4
              }}
            >
              <Typography variant="h5" id="modal-title">
                Add to Cart
              </Typography>
              <Typography variant="h6" id="modal-description">
                Quantity:
                <IconButton
                  onClick={() => setQuantity(quantity - 1)}
                  disabled={quantity === 1}
                >
                  -
                </IconButton>
                {quantity}
                <IconButton onClick={() => setQuantity(quantity + 1)}>
                  +
                </IconButton>
              </Typography>
              <Button variant="contained" onClick={handleAddToCart}>
                Add
              </Button>
            </Box>
          </Modal>
        </div>
      </StyledPage>
    </>
  );
};

export default OrderPage;
