import { useState, useEffect } from 'react';
import axios from 'axios';
import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';
const GetSuppliers = () => {


    const [data, setData] = useState([]);
    useEffect(() => {
    axios.get('http://localhost:8001/api/v/Supplier')
        .then(response => {
        setData(response.data.data);
        })
        .catch(error => {
        console.error('Error fetching data:', error);
        toast.error("Error fetching data");
        });
    }, []);
    return data;
};

export default GetSuppliers;