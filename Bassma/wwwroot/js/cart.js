// Initialize an empty cart array
let cart = [];

// Get references to the cart elements
const cartItemsContainer = document.getElementById('cart-items');
const cartTotalElement = document.getElementById('cart-total');

// Function to add items to the cart
document.querySelectorAll('.add-to-cart').forEach(button => {
    button.addEventListener('click', () => {
        const id = button.getAttribute('data-id');
        const nom = button.getAttribute('data-nom');
        const prix = parseFloat(button.getAttribute('data-prix'));
        const image = button.getAttribute('data-image'); // Get image path

        // Check if the item already exists in the cart
        const existingItem = cart.find(item => item.id == id);
        if (existingItem) {
            existingItem.quantite += 1;
            existingItem.total += prix;
        } else {
            // Add a new item to the cart
            cart.push({ id, nom, prix, quantite: 1, total: prix, image });
        }

        // Update the cart UI
        updateCartUI();
    });
});

// Function to update the cart UI
function updateCartUI() {
    cartItemsContainer.innerHTML = '';
    let total = 0;

    // Iterate over cart items and create rows dynamically
    cart.forEach(item => {
        total += item.total;
        const row = `
            <tr>
                <td style="padding: 15px;">
                    <img src="${item.image}" alt="${item.nom}" style="width: 50px; height: 50px; object-fit: cover; border-radius: 5px; margin-right: 10px;">
                    ${item.nom}
                </td>
                <td style="padding: 15px;">${item.quantite}</td>
                <td style="padding: 15px;">${item.total.toFixed(2)}</td>
                <td style="padding: 15px;">
                    <button class="btn btn-sm btn-outline-success" onclick="increaseQuantity('${item.id}')">+</button>
                    <button class="btn btn-sm btn-outline-danger" onclick="decreaseQuantity('${item.id}')">-</button>
                </td>
            </tr>
        `;
        cartItemsContainer.innerHTML += row;
    });

    // Update the total price
    cartTotalElement.textContent = total.toFixed(2);
}

// Function to increase quantity
function increaseQuantity(id) {
    const item = cart.find(item => item.id == id);
    if (item) {
        item.quantite += 1;
        item.total += item.prix;
        updateCartUI();
    }
}

// Function to decrease quantity
function decreaseQuantity(id) {
    const item = cart.find(item => item.id == id);
    if (item) {
        item.quantite -= 1;
        item.total -= item.prix;
        if (item.quantite <= 0) {
            cart = cart.filter(cartItem => cartItem.id != id);
        }
        updateCartUI();
    }
}
