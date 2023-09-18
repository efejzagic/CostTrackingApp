import React from 'react';
import { css } from "@emotion/react";
import { RingLoader } from "react-spinners";



const LoadingCoomponent = ({ loading }) => {
 
  const loadingSpinnerContainerStyles = css`
  display: flex;
  justify-content: center; /* Horizontally center */
  align-items: center; /* Vertically center */
  width: 100%; /* Set the container width to 100% */
  height: 100vh; /* Set the container height to fill the viewport */
`;

const override = css`
  display: block;
  margin: 0 auto;
  border-color: red;
`;

  return (
    <div css={loadingSpinnerContainerStyles}>
      <RingLoader color="#007BFF" loading={loading} css={override} size={75} />
    </div>
  );
};

export default LoadingCoomponent;
