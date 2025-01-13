document.getElementById('lvrForm').addEventListener('submit', async function (event) {
    event.preventDefault();

    const loanAmount = parseFloat(document.getElementById('loanAmount').value);
    const propertyValue = parseFloat(document.getElementById('propertyValue').value);

    if (loanAmount <= 0 || propertyValue <= 0) {
        alert('Loan Amount and Property Value must be greater than 0.');
        return;
    }

    const response = await fetch('https://localhost:7212/api/LvrCalculator',{
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            LoanAmount: loanAmount,
            PropertyValue: propertyValue
        })
    });

    const result = await response.json();

    if (response.ok) {
        document.getElementById('result').textContent = `LVR: ${result.lvr}%`;
    } else {
        document.getElementById('result').textContent = `Error: ${result.message}`;
    }
});
